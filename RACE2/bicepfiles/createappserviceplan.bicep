param location string
param appserviceplanName string 

resource appserviceplanresource 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: appserviceplanName
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'EP1'
    tier: 'ElasticPremium'
    size: 'EP1'
    family: 'EP'
    capacity: 1
  }
  kind: 'elastic'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: true
    maximumElasticWorkerCount: 20
    isSpot: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}
