param location string 
param vnet string
param subnetcontainerappenv string
param subnetpasaccount string
param subnetfunctionapp string

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


resource privateDnsZonesStorageAcct 'Microsoft.Network/privateDnsZones@2020-06-01' = {
  name: 'azureStgPrivateDnsZone'
  location: location
  dependsOn: [
    virtualNetworkResource
  ]
}

resource virtualNetworkLinksStorageAcct 'Microsoft.Network/privateDnsZones/virtualNetworkLinks@2020-06-01' = {
  parent: privateDnsZonesStorageAcct
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

