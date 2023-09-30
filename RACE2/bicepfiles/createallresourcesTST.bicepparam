using './createallresources.bicep'

param subscriptionid = '9327c8db-cebb-445d-9933-1bca1a76b82e'
param tenantId = '770a2450-0227-4c62-90c7-4e38537f1102'
param location = 'uksouth'
param resourcegroup = 'TSTRACINFRG1401'
param logAnalyticsWorkspaceName = 'TSTRACINFWS1401'
param race2appenvName = 'TSTRACINFAE1401'
param race2appinsightName = 'TSTRACINFAI1401'
param containerregistryName = 'TSTRACINFCR1401'
param managedidentity = 'TSTRACINFMI1401'
param vnet = 'TSTRACINFVN1401'
param subnetcontainerappenv = 'subnetcontainerappenv'
param subnetsqlserver = 'subnetsqlserver'
param subnetstorageaccount = 'subnetstorageaccount'
param subnetservicebus = 'subnetservicebus'
param subnetappconfig = 'subnetappconfig'
param subnetkeyvault = 'subnetkeyvault'
param serviceBusName = 'TSTRACINFSB1401'
param storageAccountName = 'tstracinfst1401'
param appconfigName = 'TSTRACINFAC1401'
param keyvaultName = 'TSTRACINFVT1401'
param servers_race2sqlserver_name = 'TSTRACINFSS1401'
param administratorLogin = 'race2admin'
param administratorLoginPassword = 'Race2Password123!'
param servers_race2sqldb_name = 'TSTRACINFDB1401'

