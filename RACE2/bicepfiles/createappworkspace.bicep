param location string = resourceGroup().location
param logAnalyticsWorkspaceName string

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
