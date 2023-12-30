param race2appenv string
param location string = resourceGroup().location
param lawsCustromerId string
param lawsSharedKey string
param infrastructureSubnetId string

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2023-08-01-preview' = {
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
    vnetConfiguration: {
      internal: false
      infrastructureSubnetId: infrastructureSubnetId
    }
  }

}

output id string = managedEnvironments_race2containerappenv_name_resource.id
