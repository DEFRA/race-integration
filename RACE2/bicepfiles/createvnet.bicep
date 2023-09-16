param vnet string
param subnetcontainerappenv string
param subnetsqlserver string
param subnetstorageaccount string
param subnetservicebus string

resource virtualNetwork 'Microsoft.Network/virtualNetworks@2023-05-01' = {
  name: vnet
  location: resourceGroup().location
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
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.0.0/23'
  }  
}
output subnetcontainerappenvId string = subnetcontainerappenvResource.id

resource subnetstorageaccountResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetstorageaccount
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.2.0/24'
  }
  dependsOn:[
    subnetcontainerappenvResource
  ]
}
output subnetstorageaccountId string = subnetstorageaccountResource.id

resource subnetservicebusResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetservicebus
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.3.0/24'
  }
  dependsOn:[
    subnetstorageaccountResource
  ]
}
output subnetservicebusId string = subnetservicebusResource.id

resource subnetsqlserverResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetsqlserver
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.4.0/24'
  }
  dependsOn:[
    subnetservicebusResource
  ]
}
output subnetsqlserverId string = subnetsqlserverResource.id




