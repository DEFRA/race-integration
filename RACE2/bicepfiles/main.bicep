param containerregistryName string 
param location string 
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
    resourceGroupName: resourcegroup
  } 
}

module createmanagedidentity 'createmanagedidentity.bicep' = {
  scope: resourceGroup('resourcegroupdeploy')
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
  scope: resourceGroup('resourcegroupdeploy')
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
  scope: resourceGroup('resourcegroupdeploy')
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
  scope: resourceGroup('resourcegroupdeploy')
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
