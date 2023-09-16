param vnet string
param subnetcontainerappenv string
param subnetsqlserver string
param subnetstorageaccount string
param subnetservicebus string

resource virtualNetwork 'Microsoft.Network/virtualNetworks@2021-08-01' = {
  name: vnet
  location: resourceGroup().location
  properties: {
     addressSpace: {
       addressPrefixes: [
        '10.10.0.0/16'
       ]
     }
     subnets: [
      {
        name: subnetcontainerappenv
        properties: {
          addressPrefix: '10.10.0.0/26'
        }
      }
      {
        name: subnetsqlserver
        properties: {
          addressPrefix: '10.10.0.0/26'
        }
      }
      {
        name: subnetstorageaccount
        properties: {
          addressPrefix: '10.10.0.0/26'
        }
      }
      {
        name: subnetservicebus
        properties: {
          addressPrefix: '10.10.0.0/26'
        }
      }
     ]
  }
}
