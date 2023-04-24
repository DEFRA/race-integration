param containerregistryName string 
param race2appenvName string
param location string 
param subscriptionid string 
param resourcegroup string
param managedidentity string
param logAnalyticsWorkspaceName string
param appconfigName string

module createmanagedidentity 'createmanagedidentity.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'managedidentitydeploy'
  params: {
    location: location
    miname: managedidentity
  }
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
    createmanagedidentity
  ]
}

module createappconfig 'createappconfig.bicep' = {
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
    createappworkspace
  ]
}
