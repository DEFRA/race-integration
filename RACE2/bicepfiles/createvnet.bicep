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