param storageAccountname string 
param location string = resourceGroup().location
param vnet string
param subnetstorageaccount string
param containerNames array = [
  's12reporttemplate'
  'unscannedcontent'
  'cleanfiles'
  'maliciousfiles'
]

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-05-01' existing = {
  name: vnet
}

resource subnetstorageaccountResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' existing= {
  name: subnetstorageaccount
}

resource storageAccountname_resource 'Microsoft.Storage/storageAccounts@2022-09-01' = {
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
  parent: storageAccountname_resource
}

resource storageContainers 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-01-01' = [for i in range(0, length(containerNames)):{
  name: containerNames[i]
  parent: blobServices
  properties: {
    publicAccess: 'None'
    metadata: {}
  }
}]

resource privateEndpoint 'Microsoft.Network/privateEndpoints@2023-05-01' = {
  name: 'PrivateEndpointStorageAccount'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetstorageaccountResource.name}'
    }
    privateLinkServiceConnections: [
      {
        properties: {
          privateLinkServiceId: storageAccountname_resource.id
          groupIds: [
            'blob'
          ]
        }
        name: 'PrivateEndpointStorageAccount'
      }
    ]
  }
}

resource privateDnsZones 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: 'privatelink.blob.core.windows.net'
  location: 'global'
  properties: {}
}

resource privateDnsZoneLink 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  name: '${privateDnsZones.name}/${privateDnsZones.name}-link'
  location: 'global'
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkResource.id
    }
  }
}
resource privateDnsZoneGroup 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-05-01' = {
  name: '${privateEndpoint.name}/dnsgroupname'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'storageaccountconfig'
        properties: {
          privateDnsZoneId: privateDnsZones.id
        }
      }
    ]
  }
}


