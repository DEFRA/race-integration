param containerregistryName string 
param location string 
param subscriptionid string 
param resourcegroup string
param managedidentity string
param logAnalyticsWorkspaceName string
param race2appenvName string

targetScope = 'subscription'
resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourcegroup
  location: location
}

module createmanagedidentity 'createmanagedidentity.bicep' = {
  scope: rg
  name: 'managedidentitydeploy'
  params: {
    location: location
    miname: managedidentity
  }
}

module createcontainerregistry 'createcontainerregistry.bicep' = {
  scope: rg
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

module createappworkspace 'createappworkspace.bicep' = {
  scope: rg
  name: 'appworkspacedeploy'
  params: {
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
  }
}

module createcontainerappenv 'createcontainerappenv.bicep' = {
  scope: rg
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
