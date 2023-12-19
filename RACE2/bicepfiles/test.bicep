param sites_PRDRACINFAF1401_name string = 'PRDRACINFAF1401'
param serverfarms_PRDRACINFAS1401_externalid string = '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/PRDRACINFRG1401/providers/Microsoft.Web/serverfarms/PRDRACINFAS1401'

resource sites_PRDRACINFAF1401_name_resource 'Microsoft.Web/sites@2023-01-01' = {
  name: sites_PRDRACINFAF1401_name
  location: 'UK South'
  tags: {
    ServiceCode: 'RAC'
  }
  kind: 'functionapp'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: 'prdracinfaf1401.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: 'prdracinfaf1401.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serverfarms_PRDRACINFAS1401_externalid
    reserved: false
    isXenon: false
    hyperV: false
    vnetRouteAllEnabled: false
    vnetImagePullEnabled: false
    vnetContentShareEnabled: false
    siteConfig: {
      numberOfWorkers: 1
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: false
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '8EAA32A1C9B818D1D859E3B66BDD96C167E43FA32EB83468ADB6BE2D6B0F1B25'
    containerSize: 1536
    dailyMemoryTimeQuota: 0
    httpsOnly: true
    redundancyMode: 'None'
    storageAccountRequired: false
    keyVaultReferenceIdentity: 'SystemAssigned'
  }
}

resource sites_PRDRACINFAF1401_name_ftp 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2023-01-01' = {
  parent: sites_PRDRACINFAF1401_name_resource
  name: 'ftp'
  location: 'UK South'
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    allow: true
  }
}

resource sites_PRDRACINFAF1401_name_scm 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2023-01-01' = {
  parent: sites_PRDRACINFAF1401_name_resource
  name: 'scm'
  location: 'UK South'
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    allow: true
  }
}

resource sites_PRDRACINFAF1401_name_web 'Microsoft.Web/sites/config@2023-01-01' = {
  parent: sites_PRDRACINFAF1401_name_resource
  name: 'web'
  location: 'UK South'
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    numberOfWorkers: 1
    defaultDocuments: [
      'Default.htm'
      'Default.html'
      'Default.asp'
      'index.htm'
      'index.html'
      'iisstart.htm'
      'default.aspx'
      'index.php'
    ]
    netFrameworkVersion: 'v6.0'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    remoteDebuggingVersion: 'VS2019'
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: '$PRDRACINFAF1401'
    scmType: 'None'
    use32BitWorkerProcess: true
    webSocketsEnabled: false
    alwaysOn: false
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: false
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetRouteAllEnabled: false
    vnetPrivatePortsCount: 0
    localMySqlEnabled: false
    managedServiceIdentityId: 18509
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.2'
    ftpsState: 'FtpsOnly'
    preWarmedInstanceCount: 0
    functionAppScaleLimit: 0
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 0
    azureStorageAccounts: {}
  }
}

resource sites_PRDRACINFAF1401_name_MoveMaliciousBlobEventTrigger 'Microsoft.Web/sites/functions@2023-01-01' = {
  parent: sites_PRDRACINFAF1401_name_resource
  name: 'MoveMaliciousBlobEventTrigger'
  location: 'UK South'
  properties: {
    script_href: 'https://prdracinfaf1401.azurewebsites.net/admin/vfs/site/wwwroot/RACE2VirusScanAzFnApp.dll'
    test_data_href: 'https://prdracinfaf1401.azurewebsites.net/admin/vfs/data/Functions/sampledata/MoveMaliciousBlobEventTrigger.dat'
    href: 'https://prdracinfaf1401.azurewebsites.net/admin/functions/MoveMaliciousBlobEventTrigger'
    config: {}
    language: 'dotnet-isolated'
    isDisabled: false
  }
}

resource sites_PRDRACINFAF1401_name_sites_PRDRACINFAF1401_name_azurewebsites_net 'Microsoft.Web/sites/hostNameBindings@2023-01-01' = {
  parent: sites_PRDRACINFAF1401_name_resource
  name: '${sites_PRDRACINFAF1401_name}.azurewebsites.net'
  location: 'UK South'
  properties: {
    siteName: 'PRDRACINFAF1401'
    hostNameType: 'Verified'
  }
}
