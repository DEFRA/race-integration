param location string 
param vnet string
param subnetpasaccount string
param appconfigName string

resource race2appconfig_resource 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = {
  name: appconfigName
}

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-06-01' existing = {
  name: vnet  
}

resource subnetpasResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing = {
  name: subnetpasaccount
  parent: virtualNetworkResource  
}
output subnetpasaccount string = subnetpasResource.id

resource appconfigPrivateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointAppConfig'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointAppConfig'
        properties: {
          privateLinkServiceId: race2appconfig_resource.id
          groupIds: [
            'configurationStores'
          ]
        }
      }
    ]    
  }
}
