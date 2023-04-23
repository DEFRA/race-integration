param containerregistryName string 
param location string =resourceGroup().location
param subscriptionid string 
param resourcegroup string
param managedidentity string
param logAnalyticsWorkspaceName string
param race2appenvName string

targetScope = 'subscription'
module createresourcegroup 'createresourcegroup.bicep' = {
  name: 'resourcegroupdeploy'
  params: {
    location: location
    resourceGroupName: managedidentity
  } 
}

module createmanagedidentity 'createmanagedidentity.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'managedidentitydeploy'
  params: {
    location: location
    miname: managedidentity
  }
  dependsOn: [
    createresourcegroup
  ]
}

module createcontainerregistry 'createcontainerregistry.bicep' = {
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
    createresourcegroup
    createmanagedidentity
  ]
}

module createappworkspace 'createappworkspace.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'appworkspacedeploy'
  params: {
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
  }
  dependsOn: [
    createresourcegroup
  ]
}

module createcontainerappenv 'createcontainerappenv.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'race2appenvdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    lawsCustromerId: createappworkspace.outputs.customerId
    lawsSharedKey: createappworkspace.outputs.sharedKey
  }
  dependsOn: [
    createresourcegroup
    createappworkspace
  ]
}
