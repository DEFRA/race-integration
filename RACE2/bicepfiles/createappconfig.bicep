param configurationStores_race2appconfig_name string = 'race2appconfig'
param loc string = 'westeurope'

resource configurationStores_race2appconfig_name_resource 'Microsoft.AppConfiguration/configurationStores@2022-05-01' = {
  name: configurationStores_race2appconfig_name
  location: loc
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'standard'
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/RACE2ProjectRG/providers/Microsoft.ManagedIdentity/userAssignedIdentities/Race2ManagedIdentity': {
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
