param storageAccountname string 
param location string = resourceGroup().location

param containerNames array = [
  's12reporttemplate'
  'unscannedcontent'
  'cleanfiles'
  'maliciousfiles'
]

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
    publicNetworkAccess: 'Enabled'
    allowCrossTenantReplication: false
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
    allowSharedKeyAccess: true
    networkAcls: {
      bypass: 'None'
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
        table: {
          keyType: 'Account'
          enabled: true
        }
        queue: {
          keyType: 'Account'
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }
    accessTier: 'Hot'
  }
}


resource blobServices 'Microsoft.Storage/storageAccounts/blobServices@2023-01-01' =  {
  name: 'default'
  parent: storageAccount_resource
}

resource storageContainers 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-01-01' = [for i in range(0, length(containerNames)):{
  name: containerNames[i]
  parent: blobServices
  properties: {
    publicAccess: 'None'
    metadata: {}
  }
}]
