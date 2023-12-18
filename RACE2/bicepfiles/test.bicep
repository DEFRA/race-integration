param virtualNetworks_PRDRACINFRG1401_name string = 'PRDRACINFRG1401'

resource virtualNetworks_PRDRACINFRG1401_name_resource 'Microsoft.Network/virtualNetworks@2023-06-01' = {
  name: virtualNetworks_PRDRACINFRG1401_name
  location: 'uksouth'
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    addressSpace: {
      addressPrefixes: [
        '10.10.0.0/16'
      ]
    }
    encryption: {
      enabled: false
      enforcement: 'AllowUnencrypted'
    }
    subnets: [
      {
        name: 'prdsubnet'
        id: virtualNetworks_PRDRACINFRG1401_name_prdsubnet.id
        properties: {
          addressPrefixes: [
            '10.10.2.0/24'
          ]
          serviceEndpoints: [
            {
              service: 'Microsoft.Storage'
              locations: [
                'uksouth'
                'ukwest'
              ]
            }
          ]
          delegations: []
          privateEndpointNetworkPolicies: 'Disabled'
          privateLinkServiceNetworkPolicies: 'Enabled'
          defaultOutboundAccess: true
        }
        type: 'Microsoft.Network/virtualNetworks/subnets'
      }
    ]
    virtualNetworkPeerings: []
    enableDdosProtection: false
  }
}

resource virtualNetworks_PRDRACINFRG1401_name_prdsubnet 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' = {
  name: '${virtualNetworks_PRDRACINFRG1401_name}/prdsubnet'
  properties: {
    addressPrefixes: [
      '10.10.2.0/24'
    ]
    serviceEndpoints: [
      {
        service: 'Microsoft.Storage'
        locations: [
          'uksouth'
          'ukwest'
        ]
      }
    ]
    delegations: []
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Enabled'
    defaultOutboundAccess: true
  }
  dependsOn: [
    virtualNetworks_PRDRACINFRG1401_name_resource
  ]
}
