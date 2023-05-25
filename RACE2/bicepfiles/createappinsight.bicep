param race2appinsight string
param location string = resourceGroup().location
param logAnalyticsWorkspaceid string

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: race2appinsight
  location: resourceGroup().location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspaceid
  }
}
output connectionString string = applicationInsights.properties.ConnectionString
