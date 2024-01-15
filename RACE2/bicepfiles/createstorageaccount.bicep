param storageAccountname string 
param location string = resourceGroup().location

resource storageAccount_resource 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccountname
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  } 
  properties: { 
    dnsEndpointType: 'Standard'
    defaultToOAuthAuthentication: false
    publicNetworkAccess: 'Disabled'
    allowCrossTenantReplication: false
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
    allowSharedKeyAccess: true
    networkAcls: {
      bypass: 'AzureServices'
      virtualNetworkRules: []
      ipRules: []
      defaultAction: 'Deny'
    }
    supportsHttpsTrafficOnly: true
    encryption: {
      requireInfrastructureEncryption: false
      services: {
        file: {
          keyType: 'Account'
          enabled: true
        }
        blob: {
          keyType: 'Account'
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }
    accessTier: 'Hot'
  }
}


resource unscannedcontentcontainerresource 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-09-01' = { 
  name: '${storageAccount_resource.name}/default/unscannedcontent' 
}

resource cleanfilescontainerresource 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-09-01' = { 
  name: '${storageAccount_resource.name}/default/cleanfiles' 
}

resource maliciousfilescontainerresource 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-09-01' = { 
  name: '${storageAccount_resource.name}/default/maliciousfiles' 
}

resource s12reporttemplatecontainerresource 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-09-01' = { 
  name: '${storageAccount_resource.name}/default/s12reporttemplate' 
}
