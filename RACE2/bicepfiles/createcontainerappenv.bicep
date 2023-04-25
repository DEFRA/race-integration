param race2appenv string
param location string = resourceGroup().location
param lawsCustromerId string
param lawsSharedKey string

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2022-10-01' = {
  name: race2appenv
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: lawsCustromerId
        sharedKey: lawsSharedKey
      }
    }
    zoneRedundant: false
    customDomainConfiguration: {
    }
  }
}

output id string = managedEnvironments_race2containerappenv_name_resource.id
