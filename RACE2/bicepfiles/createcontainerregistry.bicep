param containerregistryname string = 'RACE2ACR1'
param location string =resourceGroup().location
param subscriptionid string = 'd9cce027-07b6-4275-a215-dd8d52b9d469'
param resourcegroup string = 'RACE2ProjectRG1'
param managedidentity string = 'Race2ManagedIdentity'

resource race2acrresource 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: containerregistryname
  location: location
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

resource registries_Race2ACR_name_repositories_admin 'Microsoft.ContainerRegistry/registries/scopeMaps@2023-01-01-preview' = {
  parent: race2acrresource
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
  parent: race2acrresource
  name: '_repositories_pull'
  properties: {
    description: 'Can pull any repository of the registry'
    actions: [
      'repositories/*/content/read'
    ]
  }
}

resource registries_Race2ACR_name_repositories_push 'Microsoft.ContainerRegistry/registries/scopeMaps@2023-01-01-preview' = {
  parent: race2acrresource
  name: '_repositories_push'
  properties: {
    description: 'Can push to any repository of the registry'
    actions: [
      'repositories/*/content/read'
      'repositories/*/content/write'
    ]
  }
}
