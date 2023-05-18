Param (    
    [Parameter(Mandatory = $true)]
    [string]$AppRegJsonPath,
    [Parameter(Mandatory = $false)]
    [string]$AppRegManifestStorageAccountName,
    [Parameter(Mandatory = $false)]
    [string]$AppRegManifestContainerName
)

# sourcing additional functions from file
. $PSScriptRoot\..\templates\powershell\AppReg-AdditionalFunctions.ps1

Function New-AppRegSecretInAadAndKeyVault {
    Param(
        [Parameter(Mandatory = $True)]
        [Object]$headers,
        [Parameter(Mandatory = $True)]
        [Object]$app,
        [Parameter(Mandatory = $True)]
        [Object]$secret,
        [Parameter(Mandatory = $True)]
        [Object]$application,
        [Parameter(Mandatory = $True)]
        [String]$applicationsUri,
        [Parameter(Mandatory = $True)]
        [Object]$defaultProfile
    )
       
    if ($secret.type -and $secret.type -eq 'ClientSecret') {
        $passwordCredentialProperties = @{}
                    
        if ($secret.clientSecretDescriptionPrefix -and $secret.clientSecretDescriptionPrefix -ne '') {
            $passwordCredentialProperties.Add("displayName", "$($secret.clientSecretDescriptionPrefix) - ADO automatic")
        }
        else {
            $passwordCredentialProperties.Add("displayName", "ADO automatic")
        }
                    
        $defaultExpiryDateTime = ([DateTime]::UtcNow).AddMonths(6)
                    
        if ($secret.validityInDays) {
            if ($secret.validityInDays -gt 365) {
                Write-Warning "Invalid App-Reg secret validityInDays value '$($secret.validityInDays)', max value is 365. Using default validity of 6 months."
                $passwordCredentialProperties.Add("endDateTime", $defaultExpiryDateTime.ToString('yyyy-MM-ddTHH:mm:ssZ'))
            }
            else {
                $userSetExpiryDateTime = ([DateTime]::UtcNow).AddDays($secret.validityInDays)
                $passwordCredentialProperties.Add("endDateTime", $userSetExpiryDateTime.ToString('yyyy-MM-ddTHH:mm:ssZ'))
            }
        }
        else {
            $passwordCredentialProperties.Add("endDateTime", $defaultExpiryDateTime.ToString('yyyy-MM-ddTHH:mm:ssZ'))
        }

        $pwdBody = @{}
        $pwdBody.Add("passwordCredential", $passwordCredentialProperties)

        Write-Output "Creating Application Secret for '$($app.displayName)'"
        $passwordResponse = Invoke-RestMethod -Method Post -Headers $headers -Uri "$applicationsUri/$($application.id)/addPassword" -Body ($pwdBody | ConvertTo-Json -Depth 10)
        $contentType = "Secret"
        $appRegSecretValue = ConvertTo-SecureString $passwordResponse.secretText -AsPlainText -Force
        $endData = Get-Date -Date $passwordResponse.endDateTime

        Write-Output "Adding App-Reg ClientSecret to KeyVault '$($app.keyVault.name)' and secret '$($secret.key)'"
        Set-AzKeyVaultSecret -VaultName $app.keyVault.name -Name $secret.key -SecretValue $appRegSecretValue -ContentType $contentType -Expires $endData -DefaultProfile $defaultProfile | Out-Null
    }
    elseif ($secret.type -and $secret.type -eq 'ClientId') {
        $contentType = "GUID"
        $appIdValue = ConvertTo-SecureString $application.appId -AsPlainText -Force

        Write-Output "Adding App-Reg ClientId to KeyVault '$($app.keyVault.name)' and secret '$($secret.key)'"
        Set-AzKeyVaultSecret -VaultName $app.keyVault.name -Name $secret.key -SecretValue $appIdValue -ContentType $contentType -DefaultProfile $defaultProfile | Out-Null
    }
    elseif ($secret.type -and $secret.type -eq 'ClientProperty') {
        $contentType = "ClientProperty"
        $propertyValue = Invoke-Expression -Command "`$application.$($secret.propertyName)"
        $appPropertyValue = ConvertTo-SecureString $propertyValue -AsPlainText -Force

        Write-Output "Adding App-Reg ClientProperty '$($application.$($secret.propertyName))' to KeyVault '$($app.keyVault.name)' and secret '$($secret.key)'"
        Set-AzKeyVaultSecret -VaultName $app.keyVault.name -Name $secret.key -SecretValue $appPropertyValue -ContentType $contentType -DefaultProfile $defaultProfile | Out-Null
    }
    else {
        Write-Warning "Invalid App-Reg secret type '$($secret.type)'. Supported types are: 'ClientId', 'ClientSecret'and 'ClientProperty'"
    }
}

Function Rename-AadAppRegistration {
    Param(
        [Parameter(Mandatory = $True)]
        [string]$AppRegId,
        [Parameter(Mandatory = $True)]
        [string]$AppRegOriginalName,
        [Parameter(Mandatory = $True)]
        [string]$AppRegNewName,
        [Parameter(Mandatory = $True)]
        [Object]$headers,
        [Parameter(Mandatory = $True)]
        [string]$GraphApiVersion
    )

    $renameApplicationBody = @{ displayName = $AppRegNewName } | ConvertTo-Json

    $renameApplicationParams = @{
        Headers     = $headers
        Uri         = "https://graph.microsoft.com/${GraphApiVersion}/applications/${AppRegId}"
        Method      = "PATCH"
        Body        = $renameApplicationBody
        ContentType = 'application/json'
    }
    Invoke-RestMethod @renameApplicationParams
    Write-Output "AadApplication has been renamed from $($AppRegOriginalName) to $($AppRegNewName)"
}

Function Set-AadApp {
    [CmdletBinding(SupportsShouldProcess)]
    Param(
        [Parameter(Mandatory = $True)]
        [Object]$app,
        [Parameter(Mandatory = $True)]
        [string]$graphApiVersion,
        [Parameter(Mandatory = $True)]
        [Object]$HomeProfile,
        [Parameter(Mandatory = $True)]
        [Object]$ProfileForKeyVault
    )
   
    $graphApiBaseUrl = "https://graph.microsoft.com"
    $applicationsUri = "$graphApiBaseUrl/$graphApiVersion/applications"
    $servicePrincipalUri = "$graphApiBaseUrl/$graphApiVersion/servicePrincipals"
    $filter = '?$filter=displayName+eq+' + "'{0}'"

    $headers = Get-DefaultHeadersWithAccessToken -AzureTokenDomainName 'graph.microsoft.com' -DefaultProfile $HomeProfile
    $applications = Invoke-RestMethod -Method GET -Headers $headers -Uri $($applicationsUri + $filter -f $($app.displayName))
    $isAppFound = $applications.value.Length -gt 0

    if ($isAppFound -eq $false) {
        if ($app.originalDisplayName) {
            $application = Invoke-RestMethod -Method GET -Headers $headers -Uri $($applicationsUri + $filter -f $($app.originalDisplayName))
            $isAppFoundWithOriginalDisplayName = $application.value.Length -gt 0

            if ($isAppFoundWithOriginalDisplayName -eq $true) {
                $renameAadParameters = @{
                    AppRegOriginalName = $app.originalDisplayName
                    headers            = $headers
                    AppRegNewName      = $app.displayName
                    AppRegId           = $application.value.id
                    GraphApiVersion    = $graphApiVersion
                }

                Rename-AadAppRegistration @renameAadParameters
                $applications = Invoke-RestMethod -Method GET -Headers $headers -Uri $($applicationsUri + $filter -f $($app.displayName))
            }
        }
    }
    
    $isNewApp = $applications.value.Length -eq 0
    if (-not $isNewApp) {
        $application = $applications.value | Where-Object { $_.displayName -eq $app.displayName }
    }

    Write-Verbose "Building Application Body for $($app.displayName)..."
    $applicationJson = @{}
    $applicationJson.Add("displayName", $app.displayName)

    if ($app.requiredResourceAccess) {
        $requiredResourceAccess = New-Object System.Collections.ArrayList
        foreach ($requiredResource in $app.requiredResourceAccess) {
            $resource = $requiredResource
            if ($requiredResource.referenceResourceName) {
                $resourceAccess = @()
                if ($requiredResource.roles) {
                    Write-Verbose "Assigning '$($requiredResource.referenceResourceName)' Application API Roles..."
                    $applications = Invoke-RestMethod -Method GET -Headers $headers -Uri $($applicationsUri + $filter -f $($requiredResource.referenceResourceName))
                    $referenceApp = $applications.value | Where-Object { $_.displayName -eq $requiredResource.referenceResourceName }
                    foreach ($role in $requiredResource.roles) {
                        $appRole = $referenceApp.appRoles | Where-Object { $_.value -eq $role }
                        $resourceAccess += New-Object psobject -Property @{id = $appRole.id; type = 'Role' }
                    }
                    $resource = New-Object psobject -Property @{resourceAppId = $referenceApp.appId; resourceAccess = $resourceAccess }
                }
                else {
                    Write-Verbose "Assigning '$($requiredResource.referenceResourceName)' Application API Scope..."
                    $servicePrincipals = Invoke-RestMethod -Method GET -Headers $headers -Uri $($servicePrincipalUri + $filter -f $($requiredResource.referenceResourceName))
                    $servicePrincipal = $servicePrincipals.value | Where-Object { $_.displayName -eq $requiredResource.referenceResourceName }

                    $resourceAccess += New-Object psobject -Property @{id = $servicePrincipal.oauth2PermissionScopes[0].id; type = 'Scope' }
                    $resource = New-Object psobject -Property @{resourceAppId = $servicePrincipal.appId; resourceAccess = $resourceAccess }
                }
            }
            $requiredResourceAccess.Add($resource) | Out-Null
        }
        $applicationJson.Add("requiredResourceAccess", $requiredResourceAccess)
    }

    if ($app.appRoles) {
        foreach ($role in $app.appRoles) {
            if (-not $isNewApp) {
                $existingRole = $application.appRoles | Where-Object { $_.value -eq $role.value }
                if ($existingRole) {
                    $role.id = $existingRole.id
                }
            }
            if ($role.id -eq "") {
                $role.id = $(New-Guid).Guid
            }
        }
        Write-Verbose "Adding application Roles: $($app.appRoles)"
        $applicationJson.Add("appRoles", $app.appRoles)
    }

    if ($app.publicClient) {
        Write-Verbose "Setting the application as public client..."
        $applicationJson.Add("isFallbackPublicClient", $app.isPublicClient)
        $applicationJson.Add("publicClient", $app.publicClient)
    }
    if ($app.web) {
        $applicationJson.Add("web", $app.web)
    }
    if ($app.signInAudience) {
        $applicationJson.Add("signInAudience", $app.signInAudience)
    }
    else {
        $applicationJson.Add("signInAudience", "AzureADMyOrg")
    }
    if ($app.api) {
        if ($app.api.oauth2PermissionScopes) {
            if ($isNewApp) { Write-Verbose "Adding OAUTH2 Scope..." }
            foreach ($perm in $app.api.oauth2PermissionScopes) {
                $perm.id = $(New-Guid).Guid
            }
        }
        if (-not $isNewApp -and $application.api.oauth2PermissionScopes.Length -ne 0) {
            $app.api.PSObject.properties.Remove('oauth2PermissionScopes')
        }

        if ($app.api.PSObject.properties.Length -gt 0) {
            $applicationJson.Add("api", $app.api)
        }
    }

    $notes = Get-SecretRenewalNotes -app $app
    $applicationJson.Add("notes", $notes)

    $applicationBody = $applicationJson | ConvertTo-Json -Depth 100
    Write-Verbose "Payload: $($applicationBody)"
    if ($isNewApp) {
        Write-Output "Creating Application '$($app.displayName)'"
        $application = Invoke-RestMethod -Method Post -Headers $headers -Uri $applicationsUri -Body $applicationBody

        $servicePrincipals = Invoke-RestMethod -Method GET -Headers $headers -Uri $($servicePrincipalUri + $filter -f $($app.displayName))
        if ($servicePrincipals.value.Length -eq 0) {
            $spJson = @{}
            $spJson.Add("appId", $application.appId)
            if ($app.appRoles) {
                $spJson.Add("appRoleAssignmentRequired", $True)
            }

            Write-Output "Creating Service Principal for '$($app.displayName)'"
            Invoke-RestMethod -Method Post -Headers $headers -Uri $servicePrincipalUri -Body ($spJson | ConvertTo-Json -Depth 100) | Out-Null
        }

        if ($app.keyVault) {
            foreach ($secret in $app.keyVault.secrets) {
                New-AppRegSecretInAadAndKeyVault -headers $headers -app $app -applicationsUri $applicationsUri -application $application -secret $secret -DefaultProfile $ProfileForKeyVault
            }      
        }
    }
    else {
        Write-Output "Updating Application '$($app.displayName)'"
        Invoke-RestMethod -Method Patch -Headers $headers -Uri "$applicationsUri/$($application.id)" -Body $applicationBody | Out-Null

        if ($app.keyVault) {
            foreach ($secret in $app.keyVault.secrets) {          
                if ($secret.clientSecretDescriptionPrefix -and $secret.clientSecretDescriptionPrefix -ne '') {
                    $secretDisplayName = "$($secret.clientSecretDescriptionPrefix) - ADO automatic"
                }
                else {
                    $secretDisplayName = "ADO automatic"
                }
        
                $applicationSecrets = $application.passwordCredentials
                $appRegSecretsExists = $applicationSecrets | Where-Object { ($_.displayName -eq $secretDisplayName) -or ($_.displayName -eq $secret.clientSecretDescriptionPrefix) }
 
                if (-not $appRegSecretsExists) {
                    New-AppRegSecretInAadAndKeyVault -headers $headers -app $app -applicationsUri $applicationsUri -application $application -secret $secret -DefaultProfile $ProfileForKeyVault
                }
            }     
        }    
    }

    if ($app.identifierUris) {
        $app.identifierUris = $app.identifierUris -replace '{{appId}}', $application.appId
        $uris = New-Object System.Collections.ArrayList
        $uris.Add($app.identifierUris) | Out-Null
        $patchBody = @{}
        $patchBody.Add("identifierUris", $uris)

        Write-Output "Updating Identifier Uris of '$($app.displayName)'"
        Invoke-RestMethod -Method Patch -Headers $headers -Uri "$applicationsUri/$($application.id)" -Body ($patchBody | ConvertTo-Json -Depth 100) | Out-Null
    }

    # Update the Permissions after app is created
    if ($app.selfApiPermission) {
        $resourceAccess = New-Object System.Collections.ArrayList
        $resource = New-Object psobject -Property @{id = $application.api.oauth2PermissionScopes[0].id; type = "Scope" }
        $resourceAccess.Add($resource) | Out-Null

        $selfReference = New-Object psobject -Property @{resourceAppId = $application.appId; resourceAccess = $resourceAccess }
        $requiredResourceAccess.Add($selfReference) | Out-Null

        $patchBody = @{}
        $patchBody.Add("requiredResourceAccess", $requiredResourceAccess)

        Write-Output "Updating Required Resource Access of '$($app.displayName)'"
        Invoke-RestMethod -Method Patch -Headers $headers -Uri "$applicationsUri/$($application.id)" -Body ($patchBody | ConvertTo-Json -Depth 100) | Out-Null
    }
}

Function Get-AppRegRenewalAppServicePrincipalId {
    $defraCloudDevTenantId = $env:TENANT_DEFRACLOUDDEV_ID
    $o365DefraDevTenantId = $env:TENANT_DEFRADEV_ID
    $defraCloudPreTenantId = $env:TENANT_DEFRACLOUDPRE_ID
    $defraCloudTenantId = $env:TENANT_DEFRACLOUD_ID
    $defraTenantId = $env:TENANT_DEFRA_ID
    
    $currentTenantId = (Get-AzContext).Tenant.Id

    [string]$appRegRenewalAppServicePrincipalId = $null
    switch ($currentTenantId) {
        $defraCloudDevTenantId {
            $appRegRenewalAppServicePrincipalId = $env:SECRETRENEWALSPOBJECT_DEFRACLOUDDEV_ID
            break
        }
        $o365DefraDevTenantId {
            $appRegRenewalAppServicePrincipalId = $env:SECRETRENEWALSPOBJECT_O365DEFRADEV_ID
            break
        }
        $defraCloudPreTenantId {
            $appRegRenewalAppServicePrincipalId = $env:SECRETRENEWALSPOBJECT_DEFRACLOUDPRE_ID
            break
        }
        $defraCloudTenantId {
            $appRegRenewalAppServicePrincipalId = $env:SECRETRENEWALSPOBJECT_DEFRACLOUD_ID
            break
        }
        $defraTenantId {
            $subscriptionName = (Get-AzContext).Subscription.Name 

            switch -regex ($subscriptionName) {
                "^AZ[a-zA-Z]{1}-[a-zA-Z]+-(DEV|SND|TST)" {
                    $appRegRenewalAppServicePrincipalId = $env:SECRETRENEWALSPOBJECT_DEFRA_DEV_ID
                    break
                }
                "^AZ[a-zA-Z]{1}-[a-zA-Z]+-PRE" {
                    $appRegRenewalAppServicePrincipalId = $env:SECRETRENEWALSPOBJECT_DEFRA_PRE_ID
                    break
                }
                "^AZ[a-zA-Z]{1}-[a-zA-Z]+-PRD" {
                    $appRegRenewalAppServicePrincipalId = $env:SECRETRENEWALSPOBJECT_DEFRA_PRD_ID
                    break
                }
            }
        }
    }

    return $appRegRenewalAppServicePrincipalId
}

Function Get-FeatureToggleAutoSecretRenewal {
    $defraCloudDevTenantId = $env:TENANT_DEFRACLOUDDEV_ID #DefraCloudDev Dev
    $o365DefraDevTenantId = $env:TENANT_DEFRADEV_ID #DefraDev
    $defraCloudPreTenantId = $env:TENANT_DEFRACLOUDPRE_ID #DefraCloudPre PrePrd
    $defraCloudTenantId = $env:TENANT_DEFRACLOUD_ID #DefraCloud Prd
    $defraTenantId = $env:TENANT_DEFRA_ID #Defra Dev, Pre, Prd

    $currentTenantId = (Get-AzContext).Tenant.Id

    [System.Boolean]$featureToggleAutoSecretRenewal = $false
    switch ($currentTenantId) {
        $defraCloudDevTenantId {
            $featureToggleAutoSecretRenewal = $true
            break
        }
        $o365DefraDevTenantId {
            $featureToggleAutoSecretRenewal = $true
            break
        }
        $defraCloudPreTenantId {
            $featureToggleAutoSecretRenewal = $true
            break
        }
        $defraCloudTenantId {
            $featureToggleAutoSecretRenewal = $true
            break
        }
        $defraTenantId {
            $subscriptionName = (Get-AzContext).Subscription.Name 

            switch -regex ($subscriptionName) {
                "^AZ[a-zA-Z]{1}-[a-zA-Z]+-(DEV|SND|TST)" {
                    $featureToggleAutoSecretRenewal = $true
                    break
                }

                "^AZ[a-zA-Z]{1}-[a-zA-Z]+-PRE" {
                    $featureToggleAutoSecretRenewal = $true
                    break
                }

                "^AZ[a-zA-Z]{1}-[a-zA-Z]+-PRD" {
                    $featureToggleAutoSecretRenewal = $true
                    break
                }
            }
        }
    }

    return $featureToggleAutoSecretRenewal
}

Function Add-AppRegRenewalAppAsOwner {
    Param(
        [Parameter(Mandatory = $True)]
        [Object]$app,
        [Parameter(Mandatory = $True)]
        [string]$graphApiVersion,
        [Parameter(Mandatory = $True)]
        [Object]$HomeProfile
    )

    $graphApiBaseUrl = "https://graph.microsoft.com"
    $applicationsUri = "$graphApiBaseUrl/$graphApiVersion/applications"
    $headers = Get-DefaultHeadersWithAccessToken -AzureTokenDomainName 'graph.microsoft.com' -DefaultProfile $HomeProfile
    $application = Invoke-RestMethod -Method GET -Headers $headers -Uri "$($applicationsUri)?`$filter=displayName+eq+'$($app.displayName)'"
    
    $applicationsOwnersUri = "$applicationsUri/$($application.value.id)/owners"
    $getAppOwners = Invoke-RestMethod -Method GET -Uri $applicationsOwnersUri -Headers $headers

    $appRegRenewalAppServicePrincipalId = Get-AppRegRenewalAppServicePrincipalId

    if (-not [string]::IsNullOrWhiteSpace($appRegRenewalAppServicePrincipalId)) {
        if ($getAppOwners.value.id -notcontains $appRegRenewalAppServicePrincipalId) {
            Write-Output "Adding App-Reg Secret Renewal App Service Principal Id $appRegRenewalAppServicePrincipalId as an owner of App-Registration '$($app.displayName)'"
            
            $ownerBody = @{}
            $ownerBody.Add("@odata.id", "$graphApiBaseUrl/$graphApiVersion/directoryObjects/$appRegRenewalAppServicePrincipalId")
            Invoke-RestMethod -Method POST -Uri "$applicationsOwnersUri/`$ref" -Body ($ownerBody | ConvertTo-Json -Depth 10) -Headers $headers
        }
        else {
            Write-Output "App-Reg Secret Renewal App Service Principal Id $appRegRenewalAppServicePrincipalId is already an owner of App-Registration '$($app.displayName)'"
        }
    }
    else {
        Write-Warning "App-Reg Secret Renewal is not supported for the current service connection context (tenant, subscription)"
    }
}

Function Get-ProfileForKeyVault {
    Param(
        [Parameter(Mandatory = $True)]
        [Object]$app,
        [Object]$HomeTenantContext
    )

    $profileForKeyVault = $null
    if ($app.keyvault.tenant) {
        if ([string]::IsNullOrWhiteSpace($($app.keyvault.tenant.id)) -or
            [string]::IsNullOrWhiteSpace($($app.keyvault.tenant.credential.keyvaultName)) -or
            [string]::IsNullOrWhiteSpace($($app.keyvault.tenant.credential.clientId)) -or
            [string]::IsNullOrWhiteSpace($($app.keyvault.tenant.credential.secretName))) {
            throw "keyVault.tenant is incorrect, it must contain the tenant 'id' and access credentials 'credential.keyVaultName', 'credential.clientId' and 'credential.secretName'"
        }

        $appRegClientId = Get-AzKeyVaultSecret -VaultName $app.keyvault.tenant.credential.keyvaultName -Name $app.keyvault.tenant.credential.clientId
        $appRegClientIdAsPlainText = Get-SecureStringAsPlainText -SecureString $appRegClientId.SecretValue
        $appRegSecret = Get-AzKeyVaultSecret -VaultName $app.keyvault.tenant.credential.keyvaultName -Name $app.keyvault.tenant.credential.secretName
        $tenantCredential = New-Object System.Management.Automation.PSCredential($appRegClientIdAsPlainText, $appRegSecret.SecretValue)
            
        $keyVaultTenantProfile = Connect-AzAccount -Credential $tenantCredential -Tenant $app.keyvault.tenant.ID -ServicePrincipal -Scope Process
        
        # Reset context to homeTenantContext
        Set-AzContext $HomeTenantContext | Out-Null
        
        $profileForKeyVault = $keyVaultTenantProfile
    } else {
        $profileForKeyVault = $HomeTenantContext
    }

    return $profileForKeyVault
}

Function Add-AdAppRegistrations() {
    [CmdletBinding(SupportsShouldProcess)]
    Param(
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$appRegJsonPath,
        [Parameter(Mandatory = $false)]
        [string]$graphApiversion = "v1.0"
    )

    $FeatureToggleAutoSecretRenewal = Get-FeatureToggleAutoSecretRenewal

    $apps = Get-Content -Raw -Path $appRegJsonPath | ConvertFrom-Json
    
    foreach ($app in $apps.applications) {

        $profileContextForKeyVault = Get-ProfileForKeyVault -app $app -HomeTenantContext $homeTenantContext

        Set-AadApp -App $app -GraphApiVersion $GraphApiversion -HomeProfile $homeTenantContext -ProfileForKeyVault $profileContextForKeyVault

        if ($FeatureToggleAutoSecretRenewal -And [System.Convert]::ToBoolean($app.secretAutoRenewalEnabled)) {
            Add-AppRegRenewalAppAsOwner -App $app -GraphApiVersion $GraphApiversion -HomeProfile $homeTenantContext

            if ($app.keyvault.tenant) {
                $appRegRenewalAppServicePrincipalId = Get-AppRegRenewalLinkedAppServicePrincipalId -KeyVaultTenantId $app.keyvault.tenant.id -SubscriptionName $app.keyvault.tenant.subscriptionName
                Grant-SecretRenewalAppAccessToKeyVault -app $app -defaultProfile $profileContextForKeyVault -appRegRenewalAppServicePrincipalId $appRegRenewalAppServicePrincipalId
            }
            else {
                $appRegRenewalAppServicePrincipalId = Get-AppRegRenewalAppServicePrincipalId
                Grant-SecretRenewalAppAccessToKeyVault -app $app -defaultProfile $profileContextForKeyVault -appRegRenewalAppServicePrincipalId $appRegRenewalAppServicePrincipalId
            }
        }
    }
}

$homeTenantContext = Get-AzContext

if ($AppRegManifestStorageAccountName -or $AppRegManifestContainerName) {
    Write-Warning "AppRegManifestStorageAccountName and AppRegManifestContainerName are no longer required.  The Secret Renewal app now stores manifest data with the app registration in the notes property"
}

Add-AdAppRegistrations -appRegJsonPath $AppRegJsonPath
