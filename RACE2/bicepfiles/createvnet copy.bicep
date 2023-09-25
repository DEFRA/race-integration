param vnet string
param subnetcontainerappenv string
param subnetsqlserver string
param subnetstorageaccount string
param location string 

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-05-01' = {
  name: vnet
  location: location
  properties: {
     addressSpace: {
       addressPrefixes: [
        '10.10.0.0/16'
       ]
     }
     subnets: []
  }
}

resource subnetcontainerappenvResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetcontainerappenv
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.0.0/23'
  }  
}
output subnetcontainerappenvId string = subnetcontainerappenvResource.id

resource subnetstorageaccountResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetstorageaccount
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.2.0/24'
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Disabled'
  }
  dependsOn:[
    subnetcontainerappenvResource
  ]
}
output subnetstorageaccountId string = subnetstorageaccountResource.id

resource subnetsqlserverResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetsqlserver
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.3.0/24'
  }
  dependsOn:[
    subnetstorageaccountResource
  ]
}
output subnetsqlserverId string = subnetsqlserverResource.id

resource privateDnsZonesStoageAcct 'Microsoft.Network/privateDnsZones@2018-09-01' = {
  name: 'azureStgPrivateDnsZone'
  location: location
  dependsOn: [
    virtualNetworkResource
  ]
}

resource virtualNetworkLinksStorageAcct 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZonesStoageAcct
  location: location
  name: 'link-to-${virtualNetworkResource.name}'
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkResource.id
    }
  }
}

resource privateDnsZoneGroupStorageAcct 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-04-01' = {
  name: 'dnsgroupStorageAcct/storageaccount'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'configStorageAcct'
        properties: {
          privateDnsZoneId: resourceId('Microsoft.Network/privateDnsZones', privateDnsZonesSqlServer.name)
        }
      }
    ]
  }
}

resource privateDnsZonesSqlServer 'Microsoft.Network/privateDnsZones@2018-09-01' = {
  name: 'azureSqlPrivateDnsZone'
  location: location
  dependsOn: [
    virtualNetworkResource
  ]
}

resource virtualNetworkLinksSqlServer 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZonesSqlServer
  location: location
  name: 'link-to-${virtualNetworkResource.name}'
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkResource.id
    }
  }
}

resource privateDnsZoneGroupSqlServer 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-04-01' = {
  name: 'dnsgroupSqlServer/sqlserver'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'configSqlServer'
        properties: {
          privateDnsZoneId: resourceId('Microsoft.Network/privateDnsZones', privateDnsZonesSqlServer.name)
        }
      }
    ]
  }
}
