param keyvaultName string
param location string
param tenantId string
param appInsightConnectionString string
param vnet string
param subnetkeyvault string

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-05-01' existing = {
  name: vnet
}

resource subnetkeyvaultResource 'Microsoft.Network/virtualNetworks/subnets@2023-05-01' existing= {
  name: subnetkeyvault
}
resource Race2KeyVault_resource 'Microsoft.KeyVault/vaults@2022-11-01' = {
    name: keyvaultName
    location: location

    properties:{
      enableRbacAuthorization: true
      enabledForDeployment: false
      enabledForDiskEncryption: true
      enabledForTemplateDeployment: false
      tenantId: tenantId
      sku: {
        family: 'A'
        name: 'standard'
      }
      networkAcls: {
        bypass: 'AzureServices'
        defaultAction: 'Deny'
      }
    }

    resource storageNameSecret 'secrets' = {
    name: 'AppInsightsConnectionString'
    properties: {
      contentType: 'text/plain'
      value: appInsightConnectionString
    }
  } 
}

resource privateEndpoint 'Microsoft.Network/privateEndpoints@2023-05-01' = {
  name: 'PrivateEndpointKeyVault'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetkeyvaultResource.name}'
    }
    privateLinkServiceConnections: [
      {
        properties: {
          privateLinkServiceId: Race2KeyVault_resource.id
        }
        name: 'PrivateEndpointKeyVault'
      }
    ]
  }
}

  