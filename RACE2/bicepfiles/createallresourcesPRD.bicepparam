using './createallresources.bicep'

param subscriptionid = 'd9cce027-07b6-4275-a215-dd8d52b9d469'
param tenantId = '770a2450-0227-4c62-90c7-4e38537f1102'
param location = 'uksouth'
param resourcegroup = 'PRDRACINFRG1401'
param managedidentity = 'PRDRACINFMI1401'
param vnet = 'PRDRACINFVN1401'
param subnetcontainerappenv = 'subnetcontainerappenv'
param subnetsqlserver = 'subnetsqlserver'
param subnetstorageaccount = 'subnetstorageaccount'
param subnetappconfig = 'subnetappconfig'
param subnetkeyvault = 'subnetkeyvault'
param subnetacr = 'subnetacr'
param containerregistryName = 'PRDRACINFCR1401'
param storageAccountName = 'prdracinfst1401'
param appconfigName = 'PRDRACINFAC1401'
param keyvaultName = 'PRDRACINFVT1401'
param servers_race2sqlserver_name = 'PRDRACINFSS1401'
param administratorLogin = 'race2admin'
param administratorLoginPassword = 'Race2Password123!'
param servers_race2sqldb_name = 'PRDRACINFDB1401'
param logAnalyticsWorkspaceName = 'PRDRACINFWS1401'
param race2appenvName = 'PRDRACINFAE1401'
param race2appinsightName = 'PRDRACINFAI1401'
param appserviceplanName = 'PRDRACINFAS1401'
param eventgridtopicName = 'PRDRACINFEG1401'
param functionappName = 'PRDRACINFAF1401'
param subnetefgridtopic = 'subnetefgridtopic'
param subnetfunctionapp = 'subnetfunctionapp'
param adgroupname = 'AG-Azure-RAC-POC1-Race2'
param adgroupsid = '87cb0157-11f3-46aa-99ec-a1c4d4ea4c48'

