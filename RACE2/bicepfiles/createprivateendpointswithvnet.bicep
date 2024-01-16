param location string 
param vnet string
param subnetcontainerappenv string
param subnetfunctionapp string
param servers_race2sqlserver_name string
param storageAccountname string
param containerregistryname string 
param subnetpasaccount string
param appconfigName string
param keyvaultName string
param eventgridtopicName string

resource servers_race2sqlserver_name_resource 'Microsoft.Sql/servers@2023-05-01-preview' existing = {
  name: servers_race2sqlserver_name
}

resource storageAccount_resource 'Microsoft.Storage/storageAccounts@2023-01-01' existing = {
  name: storageAccountname
}

resource race2acrresource 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = {
  name: containerregistryname
}

resource race2appconfig_resource 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = {
  name: appconfigName
}

resource Race2KeyVaultResource 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyvaultName
}

resource topics_DefenderEventGridTopic_name_resource 'Microsoft.EventGrid/topics@2023-12-15-preview' existing = {
  name: eventgridtopicName
}

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-06-01' existing = {
  name: vnet  
}

resource subnetcontainerappenvResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing = {
  name: subnetcontainerappenv
  parent: virtualNetworkResource   
}
output subnetcontainerappenvId string = subnetcontainerappenvResource.id

resource subnetpasResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing = {
  name: subnetpasaccount
  parent: virtualNetworkResource  
}
output subnetpasaccount string = subnetpasResource.id

resource subnetfunctionappResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing = {
  name: subnetfunctionapp
  parent: virtualNetworkResource  
}
output subnetfunctionapp string = subnetfunctionappResource.id

resource privateEndpointSqlServer 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointSqlServer'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointSqlServer'
        properties: {
          privateLinkServiceId: servers_race2sqlserver_name_resource.id
          groupIds: [
            'sqlServer'
          ]
        }
      }
    ]
  }
}

resource privateEndpointStorageAccountBlob 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointStorageAccountBlob'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
    privateLinkServiceConnections: [
      {
        properties: {
          privateLinkServiceId: storageAccount_resource.id
          groupIds: [
            'blob'
          ]
        }
        name: 'PrivateEndpointStorageAccountBlob'
      }
    ]
  }
}

resource privateEndpointStorageAccountFile 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointStorageAccountFile'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
    privateLinkServiceConnections: [
      {
        properties: {
          privateLinkServiceId: storageAccount_resource.id
          groupIds: [
            'file'
          ]
        }
        name: 'PrivateEndpointStorageAccountFile'
      }
    ]
  }
}

resource privateEndpointStorageAccountTable 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointStorageAccountTable'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
    privateLinkServiceConnections: [
      {
        properties: {
          privateLinkServiceId: storageAccount_resource.id
          groupIds: [
            'table'
          ]
        }
        name: 'PrivateEndpointStorageAccountTable'
      }
    ]
  }
}

resource containerRegistryPrivateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointACR'
  location: location
  properties: {
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointACR'
        properties: {
          groupIds: [
            'registry'
          ]
          privateLinkServiceId: race2acrresource.id
        }
      }
    ]
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
  }
}

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
          privateLinkServiceConnectionState: {
            status: 'Approved'
            description: 'Auto-Approved'
          }
        }
      }
    ]
    
  }
}

resource keyvaultPrivateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointKeyVault'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointKeyVault'
        properties: {
          privateLinkServiceId: Race2KeyVaultResource.id
          groupIds: [
            'vault'
          ]
        }
      }
    ]
  }
}

resource eventgridtopicPrivateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointEventGridTopic'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetpasResource.name}'
    }
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointEventGridTopic'
        properties: {
          privateLinkServiceId: topics_DefenderEventGridTopic_name_resource.id
          groupIds: [
            'topic'
          ]
        }
      }
    ]
  }
}
