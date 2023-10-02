param vnet string
param subnetcontainerappenv string
param subnetsqlserver string
param subnetstorageaccount string
param subnetservicebus string
param subnetappconfig string
param subnetkeyvault string
param subnetacr string
param subnetvm string
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

resource subnetservicebusResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetservicebus
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.4.0/24'
  }
  dependsOn:[
    subnetsqlserverResource
  ]
}
output subnetservicebus string = subnetservicebusResource.id

resource subnetkeyvaultResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetkeyvault
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.5.0/24'
  }
  dependsOn:[
    subnetservicebusResource
  ]
}
output subnetkeyvault string = subnetkeyvaultResource.id

resource subnetappconfigResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
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

resource subnetacrResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
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

resource subnetvmResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetvm
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.8.0/24'
  }
  dependsOn:[
    subnetacrResource
  ]
}
output subnetvm string = subnetvmResource.id

