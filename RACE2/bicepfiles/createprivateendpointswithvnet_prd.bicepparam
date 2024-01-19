using './createprivateendpointswithvnet.bicep' /*TODO: Provide a path to a bicep template*/
param location = 'uksouth'
param vnet = 'PRDRACINFVN1401'
param subnetpasaccount = 'subnetpasaccount'
param subnetfunctionapp = 'subnetfunctionapp'
param subnetcontainerappenv = 'subnetcontainerappenv'
param storageAccountname = 'prdracinfst1401'
param servers_race2sqlserver_name = 'PRDRACINFSS1401'
param keyvaultName = 'PRDRACINFVT1401'
param appconfigName = 'PRDRACINFAC1401'
param containerregistryname = 'PRDRACINFCR1401'
param eventgridtopicName = 'PRDRACINFEG1401'





