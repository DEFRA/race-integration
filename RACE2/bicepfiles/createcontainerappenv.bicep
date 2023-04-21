param managedEnvironments_race2containerappenv_name string
param location string = resourceGroup().location
param logAnalyticsWorkspaceName string

module logAnalyticsWorkspace 'createappworkspace.bicep' = {
  name: logAnalyticsWorkspaceName
  params: {
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
    location: location
  }
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2022-10-01' = {
  name: managedEnvironments_race2containerappenv_name
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsWorkspace.outputs.customerid
        sharedKey: logAnalyticsWorkspace.outputs.sharedKey
      }
    }
    zoneRedundant: false
    customDomainConfiguration: {
    }
  }
}

output id string = managedEnvironments_race2containerappenv_name_resource.id
