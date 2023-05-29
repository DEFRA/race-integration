param location string
param logAnalyticsWorkspaceName string

resource logAnalyticsWorkspace'Microsoft.OperationalInsights/workspaces@2020-03-01-preview' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 365
    features: {
      searchVersion: 1
      enableLogAccessUsingOnlyResourcePermissions: true
    }
    workspaceCapping: {
      dailyQuotaGb: 1
    }
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

output customerId string = logAnalyticsWorkspace.properties.customerId
output sharedKey string = logAnalyticsWorkspace.listKeys().primarySharedKey
output id string = logAnalyticsWorkspace.id

