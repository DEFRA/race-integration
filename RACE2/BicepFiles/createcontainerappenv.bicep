param managedEnvironments_race2containerappenv_name string = 'race2containerappenv'
param loc string = 'westwurope'

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2022-10-01' = {
  name: managedEnvironments_race2containerappenv_name
  location: loc
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: '92b64c2e-287a-423c-b3fe-14198a7f3fd5'
      }
    }
    zoneRedundant: false
    customDomainConfiguration: {
    }
  }
}
