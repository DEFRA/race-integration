param storageAccountname string 
param location string = resourceGroup().location
param vnet string
param subnetstorageaccount string

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-06-01' existing = {
  name: vnet
}

resource subnetstorageaccountResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing= {
  name: subnetstorageaccount
}

resource storageAccount_resource 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccountname
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
  properties: {
    defaultToOAuthAuthentication: false
    allowCrossTenantReplication: false
    minimumTlsVersion: 'TLS1_0'
    allowBlobPublicAccess: true
    allowSharedKeyAccess: true
    networkAcls: {
      bypass: 'AzureServices'
      virtualNetworkRules: []
      ipRules: []
      defaultAction: 'Allow'
    }
    supportsHttpsTrafficOnly: true
    encryption: {
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

resource privateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointStorageAccount'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetstorageaccountResource.name}'
    }
    privateLinkServiceConnections: [
      {
        properties: {
          privateLinkServiceId: storageAccount_resource.id
          groupIds: [
            'blob'
          ]
        }
        name: 'PrivateEndpointStorageAccount'
      }
    ]
  }
}

