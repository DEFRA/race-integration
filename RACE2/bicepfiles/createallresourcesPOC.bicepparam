using './createallresources.bicep'

param subscriptionid = 'd9cce027-07b6-4275-a215-dd8d52b9d469'
param tenantId = '770a2450-0227-4c62-90c7-4e38537f1102'
param location = 'uksouth'
param resourcegroup = 'POCRACINFRG1401'
param managedidentity = 'POCRACINFMI1401'
param vnet = 'POCRACINFVN1401'
param subnetcontainerappenv = 'subnetcontainerappenv'
param subnetsqlserver = 'subnetsqlserver'
param subnetstorageaccount = 'subnetstorageaccount'
param subnetservicebus = 'subnetservicebus'
param subnetappconfig = 'subnetappconfig'
param subnetkeyvault = 'subnetkeyvault'
param containerregistryName = 'POCRACINFCR1401'
param serviceBusName = 'POCRACINFSB1401'
param storageAccountName = 'pocacinfst1401'
param appconfigName = 'POCRACINFAC1401'
param keyvaultName = 'POCRACINFVT1401'
param servers_race2sqlserver_name = 'POCRACINFSS1401'
param administratorLogin = 'race2admin'
param administratorLoginPassword = 'Race2Password123!'
param servers_race2sqldb_name = 'POCRACINFDB1401'
param logAnalyticsWorkspaceName = 'POCRACINFWS1401'
param race2appenvName = 'POCRACINFAE1401'
param race2appinsightName = 'POCRACINFAI1401'
