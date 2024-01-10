param location string
param functionappName string = 'ASP-POCRACINFRG1401-85f7'

resource serverfarms_ASP_POCRACINFRG1401_85f7_name_resource 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: functionappName
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
