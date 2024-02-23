using './createallresources.bicep'

param subscriptionid = 'd248d1f6-90c3-4c35-bbb9-027991742dde'
param tenantId = '6f504113-6b64-43f2-ade9-242e05780007'
param location = 'uksouth'
param resourcegroup = 'POCRACINFRG1403'
param managedidentity = 'POCRACINFMI1403'
param vnet = 'POCRACINFVN1403'
param subnetcontainerappenv = 'subnetcontainerappenv'
param subnetfunctionapp = 'subnetfunctionapp'
param subnetpasaccount = 'subnetpasaccount'
param containerregistryName = 'POCRACINFCR1403'
param storageAccountName = 'pocacinfst1403'
param appconfigName = 'POCRACINFAC1403'
param keyvaultName = 'POCRACINFVT1403'
param servers_race2sqlserver_name = 'POCRACINFSS1403'
param administratorLogin = 'race2admin'
param administratorLoginPassword = 'Race2Password123!'
param adgroupname = 'AG-Azure-RAC_POC1-ContributorsUAA'
param adgroupsid = '3af2562f-808a-4be1-b41a-d88c22e3f2ee'
param servers_race2sqldb_name = 'POCRACINFDB1403'
param logAnalyticsWorkspaceName = 'POCRACINFWS1403'
param race2appenvName = 'POCRACINFAE1403'
param race2appinsightName = 'POCRACINFAI1403'
param appserviceplanName = 'POCRACINFAS1403'
param eventgridtopicName = 'POCRACINFEG1403'
param functionappName = 'POCRACINFAF1403'

