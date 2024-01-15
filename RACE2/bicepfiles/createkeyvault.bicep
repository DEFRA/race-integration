param keyvaultName string
param location string
param tenantId string
param appInsightConnectionString string

resource Race2KeyVaultResource 'Microsoft.KeyVault/vaults@2023-07-01' = {
    name: keyvaultName
    location: location

    properties:{
      enableRbacAuthorization: true
      enabledForDeployment: false
      enabledForDiskEncryption: true
      enabledForTemplateDeployment: false
      tenantId: tenantId
      sku: {
        family: 'A'
        name: 'standard'
      }
      networkAcls: {
        bypass: 'AzureServices'
        defaultAction: 'Deny'
      }
    }

    resource storageNameSecret 'secrets' = {
    name: 'AppInsightsConnectionString'
    properties: {
      contentType: 'text/plain'
      value: appInsightConnectionString
    }
  }
} 


