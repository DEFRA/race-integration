param topics_DefenderEventGridTopic_name string = 'DefenderEventGridTopic'
param sites_RACE2DefenderScanAzurefn_externalid string = '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/POCRACINFRG1401/providers/Microsoft.Web/sites/RACE2DefenderScanAzurefn'

resource topics_DefenderEventGridTopic_name_resource 'Microsoft.EventGrid/topics@2023-12-15-preview' = {
  name: topics_DefenderEventGridTopic_name
  location: 'uksouth'
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

resource topics_DefenderEventGridTopic_name_topics_DefenderEventGridTopic_name_Subscription 'Microsoft.EventGrid/topics/eventSubscriptions@2023-12-15-preview' = {
  parent: topics_DefenderEventGridTopic_name_resource
  name: '${topics_DefenderEventGridTopic_name}Subscription'
  properties: {
    destination: {
      properties: {
        resourceId: '${sites_RACE2DefenderScanAzurefn_externalid}/functions/MoveMaliciousBlobEventTrigger'
        maxEventsPerBatch: 1
        preferredBatchSizeInKilobytes: 64
      }
      endpointType: 'AzureFunction'
    }
    filter: {
      enableAdvancedFilteringOnArrays: true
    }
    labels: []
    eventDeliverySchema: 'EventGridSchema'
    retryPolicy: {
      maxDeliveryAttempts: 30
      eventTimeToLiveInMinutes: 1440
    }
  }
}
