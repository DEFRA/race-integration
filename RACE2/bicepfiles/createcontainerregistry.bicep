param containerregistryname string 
param location string
param subscriptionid string 
param resourcegroup string
param managedidentity string
param vnet string
param subnetacr string

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-06-01' existing = {
  name: vnet
}

resource subnetacrResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing= {
  name: subnetacr
}

resource race2acrresource 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' = {
  name: containerregistryname
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'Premium'
    tier: 'Premium'
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/${subscriptionid}/resourcegroups/${resourcegroup}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/${managedidentity}': {
      }
    }
  }
  properties: {
    adminUserEnabled: true
    policies: {
      quarantinePolicy: {
        status: 'disabled'
      }
      trustPolicy: {
        type: 'Notary'
        status: 'disabled'
      }
      retentionPolicy: {
        days: 7
        status: 'disabled'
      }
      exportPolicy: {
        status: 'enabled'
      }
      azureADAuthenticationAsArmPolicy: {
        status: 'enabled'
      }
      softDeletePolicy: {
        retentionDays: 7
        status: 'disabled'
      }
    }
    encryption: {
      status: 'disabled'
    }
    dataEndpointEnabled: false
    publicNetworkAccess: 'Enabled'
    networkRuleBypassOptions: 'AzureServices'
    zoneRedundancy: 'Disabled'
    anonymousPullEnabled: false
  }
}
output registryusername string = race2acrresource.listCredentials().username
output registrypassword string = race2acrresource.listCredentials().passwords[0].value

resource containerRegistryPrivateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointACR'
  location: location
  properties: {
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointACR'
        properties: {
          groupIds: [
            'registry'
          ]
          privateLinkServiceId: race2acrresource.id
        }
      }
    ]
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetacrResource.name}'
    }
  }
}
