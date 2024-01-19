param configurationStores_prdracinfac1401_name string = 'prdracinfac1401'
param privateEndpoints_PrivateEndpointAppConfig_externalid string = '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/POCRACINFRG1401/providers/Microsoft.Network/privateEndpoints/PrivateEndpointAppConfig'

resource configurationStores_prdracinfac1401_name_resource 'Microsoft.AppConfiguration/configurationStores@2023-03-01' = {
  name: configurationStores_prdracinfac1401_name
  location: 'uksouth'
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'standard'
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourcegroups/POCRACINFRG1401/providers/Microsoft.ManagedIdentity/userAssignedIdentities/PRDRACINFMI1401': {}
    }
  }
  properties: {
    encryption: {}
    disableLocalAuth: false
    softDeleteRetentionInDays: 7
    enablePurgeProtection: false
  }
}

resource configurationStores_prdracinfac1401_name_PrivateEndpointAppConfig 'Microsoft.AppConfiguration/configurationStores/privateEndpointConnections@2023-03-01' = {
  parent: configurationStores_prdracinfac1401_name_resource
  name: 'PrivateEndpointAppConfig'
  properties: {
    privateEndpoint: {
      id: privateEndpoints_PrivateEndpointAppConfig_externalid
    }
    privateLinkServiceConnectionState: {
      status: 'Approved'
      description: 'Auto-Approved'
    }
  }
}
