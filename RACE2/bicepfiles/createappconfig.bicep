param appconfigName string
param location string = resourceGroup().location
param subscriptionid string 
param resourcegroup string
param managedidentity string
param vnet string
param subnetappconfig string

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-06-01' existing = {
  name: vnet
}

resource subnetappconfigResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing= {
  name: subnetappconfig
}

resource race2appconfig_resource 'Microsoft.AppConfiguration/configurationStores@2023-03-01' = {
  name: appconfigName
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'standard'
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/${subscriptionid}/resourcegroups/${resourcegroup}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/${managedidentity}': {
      }
    }
  }
  properties: {
    encryption: {
    }
    disableLocalAuth: false
    softDeleteRetentionInDays: 7
    enablePurgeProtection: false
  }
}

resource appconfigPrivateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointAppConfig'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetappconfigResource.name}'
    }
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointAppConfig'
        properties: {
          privateLinkServiceId: race2appconfig_resource.id
          groupIds: [
            'appConfig'
          ]
        }
      }
    ]
  }
}

