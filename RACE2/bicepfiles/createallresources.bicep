param subscriptionid string 
param tenantId string =  subscription().tenantId
param location string = resourceGroup().location
param resourcegroup string
param managedidentity string
param vnet string
param subnetcontainerappenv string
param subnetpasaccount string
param subnetfunctionapp string
param servers_race2sqlserver_name string
param servers_race2sqldb_name string
param containerregistryName string
@secure()
param administratorLogin string
@secure()
param administratorLoginPassword string
param appconfigName string
param keyvaultName string
param appserviceplanName string
param eventgridtopicName string
param functionappName string
param storageAccountName string
param logAnalyticsWorkspaceName string
param race2appenvName string
param race2appinsightName string
param adgroupname string
param adgroupsid string

module createmanagedidentitymodule 'createmanagedidentity.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'managedidentitydeploy'
  params: {
    location: location
    miname: managedidentity
  }
}

module createvnetmodule 'createvnet.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'vnetdeploy'
  params: {
    vnet: vnet
    subnetcontainerappenv: subnetcontainerappenv
    subnetpasaccount: subnetpasaccount
    location: location 
    subnetfunctionapp: subnetfunctionapp
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
    createvnetmodule
  ]
}

module createappserviceplanmodule 'createappserviceplan.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'appserviceplandeploy'
  params: {
    location: location
    appserviceplanName: appserviceplanName
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
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
    tenantId: tenantId
    adgroupname: adgroupname
    adgroupsid: adgroupsid

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

module createcontainerappenvmodule 'createcontainerappenv.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'race2appenvdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    lawsCustromerId: createappworkspacemodule.outputs.customerId
    lawsSharedKey: createappworkspacemodule.outputs.sharedKey
    infrastructureSubnetId: createvnetmodule.outputs.subnetcontainerappenvId
  }
  dependsOn: [
    createappworkspacemodule
  ]
}

module createfunctionappmodule 'createfunctionapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'functionappdeploy'
  params: {
    subscriptionid: subscriptionid
    location: location
    resourcegroup: resourcegroup
    managedidentity: managedidentity
    functionappName: functionappName
    race2appinsightName: race2appinsightName
    appserviceplanName: appserviceplanName
    storageAccountName: storageAccountName
  }
  dependsOn: [
    createappserviceplanmodule
    createappinsightmodule
    createstorageaccountmodule
  ]
}

module createeventgridtopicmodule 'createeventgridtopic.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'eventgridtopicdeploy'
  params: {
    location: location
    eventgridtopicName: eventgridtopicName
  }
  dependsOn: [
    createfunctionappmodule
  ]
}

module createprivateendpointswithvnetmodule 'createprivateendpointswithvnet.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'privateendpointswithvnetdeploy'
  params: {
    vnet: vnet
    subnetcontainerappenv: subnetcontainerappenv
    subnetpasaccount: subnetpasaccount
    location: location 
    appconfigName: appconfigName
    keyvaultName: keyvaultName
    containerregistryname: containerregistryName
    eventgridtopicName: eventgridtopicName
    storageAccountname: storageAccountName
    subnetfunctionapp: subnetfunctionapp
    servers_race2sqlserver_name: servers_race2sqlserver_name
  }
  dependsOn: [
    createappconfigmodule
    createkeyvaultmodule
    createeventgridtopicmodule
    createsqlservermodule
    createstorageaccountmodule
    createcontainerregistrymodule
  ]
}


