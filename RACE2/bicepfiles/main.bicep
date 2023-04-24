param subscriptionid string 
param tenantId string
param location string
param containerregistryName string 
param race2appenvName string
param namespaces_ServiceBus_name string
param storageAccountName string
param resourcegroup string
param managedidentity string
param logAnalyticsWorkspaceName string
param appconfigName string
param keyvaultName string

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
    namespaces_ServiceBus_name: namespaces_ServiceBus_name
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createstorageaccountmodule 'createstorageaccount.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'stoareacoountdeploy'
  params: {
    location: location
    storageAccountname: storageAccountName
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
  }
  dependsOn: [
    createappworkspacemodule
  ]
}
