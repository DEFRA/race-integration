param serverfarms_ASP_PRDRACINFRG1401_a408_name string = 'ASP-PRDRACINFRG1401-a408'

resource serverfarms_ASP_PRDRACINFRG1401_a408_name_resource 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: serverfarms_ASP_PRDRACINFRG1401_a408_name
  location: 'UK South'
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
    reserved: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}
