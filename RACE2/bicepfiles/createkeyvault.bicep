param vaults_Race2KeyVault_name string = 'Race2KeyVault'
param loc string = 'westeurope'

resource vaults_Race2KeyVault_name_resource 'Microsoft.KeyVault/vaults@2022-11-01' = {
  name: vaults_Race2KeyVault_name
  location: loc
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: '770a2450-0227-4c62-90c7-4e38537f1102'
    accessPolicies: [
      {
        tenantId: '770a2450-0227-4c62-90c7-4e38537f1102'
        objectId: '160b1ec3-b830-4c1c-8e01-9039cf27364a'
        permissions: {
          keys: [
            'Get'
            'List'
            'Update'
            'Create'
            'Import'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
            'GetRotationPolicy'
            'SetRotationPolicy'
            'Rotate'
          ]
          secrets: [
            'Get'
            'List'
            'Set'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
          ]
          certificates: [
            'Get'
            'List'
            'Update'
            'Create'
            'Import'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
            'ManageContacts'
            'ManageIssuers'
            'GetIssuers'
            'ListIssuers'
            'SetIssuers'
            'DeleteIssuers'
          ]
        }
      }
      {
        tenantId: '770a2450-0227-4c62-90c7-4e38537f1102'
        objectId: '34e4a290-4d1e-443d-a5ed-360038f56172'
        permissions: {
          keys: [
            'Get'
            'List'
            'Update'
            'Create'
            'Import'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
            'GetRotationPolicy'
            'SetRotationPolicy'
            'Rotate'
          ]
          secrets: [
            'Get'
            'List'
            'Set'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
          ]
          certificates: [
            'Get'
            'List'
            'Update'
            'Create'
            'Import'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
            'ManageContacts'
            'ManageIssuers'
            'GetIssuers'
            'ListIssuers'
            'SetIssuers'
            'DeleteIssuers'
          ]
        }
      }
    ]
    enabledForDeployment: true
    enabledForDiskEncryption: true
    enabledForTemplateDeployment: true
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    enableRbacAuthorization: true
    vaultUri: 'https://race2keyvault.vault.azure.net/'
    provisioningState: 'Succeeded'
    publicNetworkAccess: 'Enabled'
  }
}

resource vaults_Race2KeyVault_name_ADO_DefraGovUK_AZD_RAC_POC1 'Microsoft.KeyVault/vaults/secrets@2022-11-01' = {
  parent: vaults_Race2KeyVault_name_resource
  name: 'ADO-DefraGovUK-AZD-RAC-POC1'
  location: loc
  properties: {
    attributes: {
      enabled: true
      exp: 1741951481
    }
  }
}

resource vaults_Race2KeyVault_name_SqlServerConnectionDev 'Microsoft.KeyVault/vaults/secrets@2022-11-01' = {
  parent: vaults_Race2KeyVault_name_resource
  name: 'SqlServerConnectionDev'
  location: loc
  properties: {
    attributes: {
      enabled: true
    }
  }
}

resource vaults_Race2KeyVault_name_SqlServerConnString 'Microsoft.KeyVault/vaults/secrets@2022-11-01' = {
  parent: vaults_Race2KeyVault_name_resource
  name: 'SqlServerConnString'
  location: loc
  properties: {
    attributes: {
      enabled: true
    }
  }
}
