param storageAccountname string 
param location string = resourceGroup().location

param containerNames array = [
  's12reporttemplate'
  'unscannedcontent'
  'cleanfiles'
  'maliciousfiles'
]

resource storageAccount_resource 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: storageAccountname
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'Standard_ZRS'
  }
  kind: 'StorageV2'
  properties: {
    dnsEndpointType: 'Standard'
    defaultToOAuthAuthentication: false
    publicNetworkAccess: 'Enabled'
    allowCrossTenantReplication: true
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: true
    allowSharedKeyAccess: true
    networkAcls: {
      bypass: 'None'
      virtualNetworkRules: []
      ipRules: []
      defaultAction: 'Allow'
    }
    supportsHttpsTrafficOnly: true
    encryption: {
      requireInfrastructureEncryption: false      
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
    publicAccess: 'Container'
    metadata: {}
  }
}]
