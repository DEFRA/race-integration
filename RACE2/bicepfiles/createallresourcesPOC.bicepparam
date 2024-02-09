using './createallresources.bicep'

param subscriptionid = 'd248d1f6-90c3-4c35-bbb9-027991742dde'
param tenantId = '6f504113-6b64-43f2-ade9-242e05780007'
param location = 'uksouth'
param resourcegroup = 'POCRACINFRG1401'
param managedidentity = 'POCRACINFMI1402'
param vnet = 'POCRACINFVN1402'
param subnetcontainerappenv = 'subnetcontainerappenv'
param containerregistryName = 'POCRACINFCR1402'
param storageAccountName = 'pocracinfst1402'
param appconfigName = 'POCRACINFAC1402'
param keyvaultName = 'POCRACINFVT1402'
param servers_race2sqlserver_name = 'POCRACINFSS1401'
param administratorLogin = 'race2admin'
param administratorLoginPassword = 'Race2Password123!'
param servers_race2sqldb_name = 'POCRACINFDB1401'
param logAnalyticsWorkspaceName = 'POCRACINFWS1402'
param race2appenvName = 'POCRACINFAE1402'
param race2appinsightName = 'POCRACINFAI1402'
param appserviceplanName = 'POCRACINFAS1402'
param eventgridtopicName = 'POCRACINFEG1402'
param functionappName = 'POCRACINFAF1402'
param subnetfunctionapp = 'subnetfunctionapp'
param adgroupname = 'AG-Azure-RAC_POC1-ContributorsUAA'
param adgroupsid = '3af2562f-808a-4be1-b41a-d88c22e3f2ee'
param subnetpasaccount = 'subnetpasaccount'
