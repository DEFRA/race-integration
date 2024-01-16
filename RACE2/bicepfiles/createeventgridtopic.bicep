param location string
param eventgridtopicName string 

resource topics_DefenderEventGridTopic_name_resource 'Microsoft.EventGrid/topics@2023-12-15-preview' = {
  name: eventgridtopicName
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'Basic'
  }
  kind: 'Azure'
  identity: {
    type: 'None'
  }
  properties: {
    minimumTlsVersionAllowed: '1.0'
    inputSchema: 'EventGridSchema'
    publicNetworkAccess: 'Enabled'
    inboundIpRules: []
    disableLocalAuth: false
    dataResidencyBoundary: 'WithinGeopair'
  }
}

