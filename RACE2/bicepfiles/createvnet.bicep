param location string 
param vnet string
param subnetcontainerappenv string
param subnetpasaccount string
param subnetfunctionapp string

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

resource subnetpasResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetpasaccount
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.2.0/24'
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Disabled'
  }  
}
output subnetpasaccount string = subnetpasResource.id

resource subnetfunctionappResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: subnetfunctionapp
  parent: virtualNetworkResource
  properties: {
    addressPrefix: '10.10.3.0/24'    
    delegations: []
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Enabled'
  }
  dependsOn:[
    subnetpasResource
  ]
}
output subnetfunctionapp string = subnetfunctionappResource.id
