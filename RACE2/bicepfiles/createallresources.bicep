param subscriptionid string 
param tenantId string =  subscription().tenantId
param location string = resourceGroup().location
param resourcegroup string
param managedidentity string
param servers_race2sqlserver_name string
param servers_race2sqldb_name string
param containerregistryName string
@secure()
param administratorLogin string
@secure()
param administratorLoginPassword string
param appconfigName string
param keyvaultName string
param serviceBus_name string
param storageAccountName string
param logAnalyticsWorkspaceName string
param race2appenvName string
param race2appinsightName string
module createmanagedidentitymodule 'createmanagedidentity.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'managedidentitydeploy'
  params: {
    location: location
    miname: managedidentity
  }
}

module createcontainerregistrymodule 'createcontainerregistry.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'containerregistrydeploy'
  params: {
    location: location
    subscriptionid: subscriptionid
    resourcegroup: resourcegroup
    containerregistryname: containerregistryName
    managedidentity: managedidentity
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createservicebusmodule 'createservicebus.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'servicebusdeploy'
  params: {
    location: location
    serviceBus_name: serviceBus_name
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createstorageaccountmodule 'createstorageaccount.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'storageaccountdeploy'
  params: {
    location: location
    storageAccountname: storageAccountName
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createappconfigmodule 'createappconfig.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'appconfigdeploy'
  params: {
    location: location
    subscriptionid: subscriptionid
    resourcegroup: resourcegroup
    appconfigName: appconfigName
    managedidentity: managedidentity
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createkeyvaultmodule 'createkeyvault.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'keyvaultdeploy'
  params: {
    location: location
    keyvaultName: keyvaultName
    tenantId: tenantId
    appInsightConnectionString: createappinsightmodule.outputs.connectionString
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createsqlservermodule 'createsqlserver.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'sqlserverdeploy'
  params: {
    location: location
    servers_race2sqlserver_name: servers_race2sqlserver_name
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    servers_race2sqldb_name: servers_race2sqldb_name
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createappworkspacemodule 'createappworkspace.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'appworkspacedeploy'
  params: {
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
  }
}

module createappinsightmodule 'createappinsight.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'appinsightdeploy'
  params: {
    location: location
    logAnalyticsWorkspaceid: createappworkspacemodule.outputs.id
    race2appinsight: race2appinsightName
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createcontainerappenvmodule 'createcontainerappenv.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'race2appenvdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    lawsCustromerId: createappworkspacemodule.outputs.customerId
    lawsSharedKey: createappworkspacemodule.outputs.sharedKey
  }
  dependsOn: [
    createappworkspacemodule
  ]
}


