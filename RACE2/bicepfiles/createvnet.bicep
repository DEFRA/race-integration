param location string 
param vnet string
param subnetcontainerappenv string
param subnetsqlserver string
param subnetstorageaccount string
param subnetfunctionapp string
param subnetappconfig string
param subnetkeyvault string
param subnetacr string
param subnetefgridtopic string

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-06-01' = {
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

resource subnetcontainerappenvResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetcontainerappenv
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.0.0/23'
  }  
}
output subnetcontainerappenvId string = subnetcontainerappenvResource.id

resource subnetstorageaccountResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
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

resource privateDnsZonesStoageAcct 'Microsoft.Network/privateDnsZones@2020-06-01' = {
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

resource privateDnsZoneGroupStorageAcct 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-06-01' = {
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

resource subnetsqlserverResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
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

resource privateDnsZonesSqlServer 'Microsoft.Network/privateDnsZones@2020-06-01' = {
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

resource subnetefgridtopicResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetefgridtopic
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.4.0/24'
  }
  dependsOn:[
    subnetsqlserverResource
  ]
}
output subnetefgridtopic string = subnetefgridtopicResource.id

resource privateDnsZonesEFGridTopic 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: 'azureEFGPrivateDnsZone'
  location: location
  dependsOn: [
    virtualNetworkResource
  ]
}

resource virtualNetworkLinksEFGridTopic 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZonesEFGridTopic
  location: location
  name: 'link-to-${virtualNetworkResource.name}'
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkResource.id
    }
  }
}

resource privateDnsZoneGroupEFGridTopic 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-06-01' = {
  name: 'dnsgroupEFGridTopic/efgridtopic'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'configStorageAcct'
        properties: {
          privateDnsZoneId: resourceId('Microsoft.Network/privateDnsZones', privateDnsZonesEFGridTopic.name)
        }
      }
    ]
  }
}

resource subnetkeyvaultResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetkeyvault
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.5.0/24'
  }
  dependsOn:[
    subnetefgridtopicResource
  ]
}
output subnetkeyvault string = subnetkeyvaultResource.id

resource privateDnsZonesKeyVault 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: 'azureKVPrivateDnsZone'
  location: location
  dependsOn: [
    virtualNetworkResource
  ]
}

resource virtualNetworkLinksKeyVault 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZonesKeyVault
  location: location
  name: 'link-to-${virtualNetworkResource.name}'
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkResource.id
    }
  }
}

resource privateDnsZoneGroupKeyVault 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-06-01' = {
  name: 'dnsgroupKeyVault/keyvault'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'configKeyVault'
        properties: {
          privateDnsZoneId: resourceId('Microsoft.Network/privateDnsZones', privateDnsZonesKeyVault.name)
        }
      }
    ]
  }
}

resource subnetappconfigResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetappconfig
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.6.0/24'
  }
  dependsOn:[
    subnetkeyvaultResource
  ]
}
output subnetappconfig string = subnetappconfigResource.id

resource privateDnsZonesAppConfig 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: 'azureAppConfigPrivateDnsZone'
  location: location
  dependsOn: [
    virtualNetworkResource
  ]
}

resource virtualNetworkLinksAppConfig 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZonesAppConfig
  location: location
  name: 'link-to-${virtualNetworkResource.name}'
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkResource.id
    }
  }
}

resource privateDnsZoneGroupAppConfig 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-06-01' = {
  name: 'dnsgroupAppConfig/appconfig'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'configAppConfig'
        properties: {
          privateDnsZoneId: resourceId('Microsoft.Network/privateDnsZones', privateDnsZonesAppConfig.name)
        }
      }
    ]
  }
}

resource subnetacrResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetacr
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.7.0/24'
  }
  dependsOn:[
    subnetappconfigResource
  ]
}
output subnetacr string = subnetacrResource.id

resource privateDnsZonesACR 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: 'azureACRPrivateDnsZone'
  location: location
  dependsOn: [
    virtualNetworkResource
  ]
}

resource virtualNetworkLinksACR 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZonesACR
  location: location
  name: 'link-to-${virtualNetworkResource.name}'
  properties: {
    registrationEnabled: false
    virtualNetwork: {
      id: virtualNetworkResource.id
    }
  }
}

resource privateDnsZoneGroupACR 'Microsoft.Network/privateEndpoints/privateDnsZoneGroups@2023-06-01' = {
  name: 'dnsgroupACR/acr'
  properties: {
    privateDnsZoneConfigs: [
      {
        name: 'configACR'
        properties: {
          privateDnsZoneId: resourceId('Microsoft.Network/privateDnsZones', privateDnsZonesACR.name)
        }
      }
    ]
  }
}

resource subnetfunctionappResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetfunctionapp
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.8.0/24'
    serviceEndpoints: [
      {
        service: 'Microsoft.ContainerRegistry'
        locations: [
          '*'
        ]
      }
      {
        service: 'Microsoft.KeyVault'
        locations: [
          '*'
        ]
      }
      {
        service: 'Microsoft.Sql'
        locations: [
          'uksouth'
        ]
      }
      {
        service: 'Microsoft.Storage'
        locations: [
          'uksouth'
          'ukwest'
        ]
      }
      {
        service: 'Microsoft.Web'
        locations: [
          '*'
        ]
      }
    ]
    delegations: []
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Enabled'
  }
  dependsOn:[
    subnetacrResource
  ]
}
output subnetfunctionapp string = subnetfunctionappResource.id
