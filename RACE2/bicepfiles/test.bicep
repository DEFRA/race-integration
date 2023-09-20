param virtualNetworks_POCRACINFVN1401_name string = 'POCRACINFVN1401'

resource virtualNetworks_POCRACINFVN1401_name_resource 'Microsoft.Network/virtualNetworks@2023-04-01' = {
  name: virtualNetworks_POCRACINFVN1401_name
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
    subnets: [
      {
        name: 'subnetsqlserver'
        id: virtualNetworks_POCRACINFVN1401_name_subnetsqlserver.id
        properties: {
          addressPrefix: '10.10.0.0/24'
          serviceEndpoints: [
            {
              service: 'Microsoft.Sql'
              locations: [
                'uksouth'
              ]
            }
          ]
          delegations: []
          privateEndpointNetworkPolicies: 'Disabled'
          privateLinkServiceNetworkPolicies: 'Enabled'
        }
        type: 'Microsoft.Network/virtualNetworks/subnets'
      }
      {
        name: 'subnetcontainerappenvResource'
        properties: {
          addressPrefix: '10.10.1.0/24'
          serviceEndpoints: [
            {
              service: 'Microsoft.Web'
              locations: [
                '*'
              ]
            }
          ]
          delegations: []
          privateEndpointNetworkPolicies: 'Disabled'
          privateLinkServiceNetworkPolicies: 'Enabled'
        }
        type: 'Microsoft.Network/virtualNetworks/subnets'
      }
      {
        name: 'subnetservicebusResource'
        properties: {
          addressPrefix: '10.10.2.0/24'
          serviceEndpoints: [
            {
              service: 'Microsoft.ServiceBus'
              locations: [
                '*'
              ]
            }
          ]
          delegations: []
          privateEndpointNetworkPolicies: 'Disabled'
          privateLinkServiceNetworkPolicies: 'Enabled'
        }
        type: 'Microsoft.Network/virtualNetworks/subnets'
      }
      {
        name: 'subnetstorageaccountResource'
        properties: {
          addressPrefix: '10.10.3.0/24'
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
        }
        type: 'Microsoft.Network/virtualNetworks/subnets'
      }
    ]
    virtualNetworkPeerings: []
    enableDdosProtection: false
  }
}

resource virtualNetworks_POCRACINFVN1401_name_subnetcontainerappenvResource 'Microsoft.Network/virtualNetworks/subnets@2023-04-01' = {
  name: '${virtualNetworks_POCRACINFVN1401_name}/subnetcontainerappenvResource'
  properties: {
    addressPrefix: '10.10.1.0/24'
    serviceEndpoints: [
      {
        service: 'Microsoft.Web'
        locations: [
          '*'
        ]
      }
    ]
    delegations: []
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Enabled'
  }
  dependsOn: [
    virtualNetworks_POCRACINFVN1401_name_resource
  ]
}

resource virtualNetworks_POCRACINFVN1401_name_subnetservicebusResource 'Microsoft.Network/virtualNetworks/subnets@2023-04-01' = {
  name: '${virtualNetworks_POCRACINFVN1401_name}/subnetservicebusResource'
  properties: {
    addressPrefix: '10.10.2.0/24'
    serviceEndpoints: [
      {
        service: 'Microsoft.ServiceBus'
        locations: [
          '*'
        ]
      }
    ]
    delegations: []
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Enabled'
  }
  dependsOn: [
    virtualNetworks_POCRACINFVN1401_name_resource
  ]
}

resource virtualNetworks_POCRACINFVN1401_name_subnetsqlserver 'Microsoft.Network/virtualNetworks/subnets@2023-04-01' = {
  name: '${virtualNetworks_POCRACINFVN1401_name}/subnetsqlserver'
  properties: {
    addressPrefix: '10.10.0.0/24'
    serviceEndpoints: [
      {
        service: 'Microsoft.Sql'
        locations: [
          'uksouth'
        ]
      }
    ]
    delegations: []
    privateEndpointNetworkPolicies: 'Disabled'
    privateLinkServiceNetworkPolicies: 'Enabled'
  }
  dependsOn: [
    virtualNetworks_POCRACINFVN1401_name_resource
  ]
}

resource virtualNetworks_POCRACINFVN1401_name_subnetstorageaccountResource 'Microsoft.Network/virtualNetworks/subnets@2023-04-01' = {
  name: '${virtualNetworks_POCRACINFVN1401_name}/subnetstorageaccountResource'
  properties: {
    addressPrefix: '10.10.3.0/24'
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
  }
  dependsOn: [
    virtualNetworks_POCRACINFVN1401_name_resource
  ]
}
