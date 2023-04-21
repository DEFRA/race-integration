param location string
param logAnalyticsWorkspaceName string
param azureServiceConnection string
param resourceGroupName string
param managedEnvironments_race2containerappenv_name string
param containerregistryname string

resource logAnalyticsWorkspace'Microsoft.OperationalInsights/workspaces@2020-03-01-preview' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: any({
    retentionInDays: 365
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

output customerid string = logAnalyticsWorkspace.id
output sharedKey string = logAnalyticsWorkspace.listKeys().primarySharedKey
