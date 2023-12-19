param serverfarms_PRDRACINFAP1401_name string = 'PRDRACINFAP1401'

resource serverfarms_PRDRACINFAP1401_name_resource 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: serverfarms_PRDRACINFAP1401_name
  location: 'UK South'
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'S1'
    tier: 'Standard'
    size: 'S1'
    family: 'S'
    capacity: 1
  }
  kind: 'app'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}
