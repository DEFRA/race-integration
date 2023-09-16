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

resource subnetservicebusResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetservicebus
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.0.0/23'
  }
}

resource subnetsqlserverResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetsqlserver
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.0.0/23'
  }
  dependsOn: [
    subnetservicebusResource
  ]
}

resource subnetstorageaccountResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetstorageaccount
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.0.0/23'
  }
  dependsOn: [
    subnetsqlserverResource
  ]
}

resource subnetcontainerappenvResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' = {
  name: subnetcontainerappenv
  parent: virtualNetwork
  properties: {
    addressPrefix: '10.10.0.0/23'
  }
  dependsOn: [
    subnetstorageaccountResource
  ]
}
