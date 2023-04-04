param registries_Race2ACR_name string = 'Race2ACR'
param loc string = 'westeurope'

resource registries_Race2ACR_name_resource 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: registries_Race2ACR_name
  location: loc
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourcegroups/RACE2ProjectRG/providers/Microsoft.ManagedIdentity/userAssignedIdentities/Race2ManagedIdentity': {
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

resource registries_Race2ACR_name_repositories_admin 'Microsoft.ContainerRegistry/registries/scopeMaps@2023-01-01-preview' = {
  parent: registries_Race2ACR_name_resource
  name: '_repositories_admin'
  properties: {
    description: 'Can perform all read, write and delete operations on the registry'
    actions: [
      'repositories/*/metadata/read'
      'repositories/*/metadata/write'
      'repositories/*/content/read'
      'repositories/*/content/write'
      'repositories/*/content/delete'
    ]
  }
}

resource registries_Race2ACR_name_repositories_pull 'Microsoft.ContainerRegistry/registries/scopeMaps@2023-01-01-preview' = {
  parent: registries_Race2ACR_name_resource
  name: '_repositories_pull'
  properties: {
    description: 'Can pull any repository of the registry'
    actions: [
      'repositories/*/content/read'
    ]
  }
}

resource registries_Race2ACR_name_repositories_push 'Microsoft.ContainerRegistry/registries/scopeMaps@2023-01-01-preview' = {
  parent: registries_Race2ACR_name_resource
  name: '_repositories_push'
  properties: {
    description: 'Can push to any repository of the registry'
    actions: [
      'repositories/*/content/read'
      'repositories/*/content/write'
    ]
  }
}
