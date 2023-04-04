param namespaces_Race2ServiceBus_name string = 'Race2ServiceBus'
param loc string = 'westeurope'

resource namespaces_Race2ServiceBus_name_resource 'Microsoft.ServiceBus/namespaces@2022-10-01-preview' = {
  name: namespaces_Race2ServiceBus_name
  location: loc
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
  properties: {
    premiumMessagingPartitions: 0
    minimumTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    disableLocalAuth: false
    zoneRedundant: false
  }
}

resource namespaces_Race2ServiceBus_name_RootManageSharedAccessKey 'Microsoft.ServiceBus/namespaces/authorizationrules@2022-10-01-preview' = {
  parent: namespaces_Race2ServiceBus_name_resource
  name: 'RootManageSharedAccessKey'
  location: loc
  properties: {
    rights: [
      'Listen'
      'Manage'
      'Send'
    ]
  }
}

resource namespaces_Race2ServiceBus_name_default 'Microsoft.ServiceBus/namespaces/networkRuleSets@2022-10-01-preview' = {
  parent: namespaces_Race2ServiceBus_name_resource
  name: 'default'
  location: loc
  properties: {
    publicNetworkAccess: 'Enabled'
    defaultAction: 'Allow'
    virtualNetworkRules: []
    ipRules: []
  }
}
