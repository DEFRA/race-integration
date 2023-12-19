param location string
param appserviceplanName string = 'ASP-POCRACINFRG1401-85f7'

resource appserviceplanresource 'Microsoft.Web/serverfarms@2022-09-01' = {
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
    reserved: true
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}
