param containerregistryname string 
param location string =resourceGroup().location
param subscriptionid string 
param resourcegroup string
param managedidentity string

targetScope = 'subscription'
module createresourcegroup 'createresourcegroup.bicep' = {
  name: resourcegroup
  params: {
    location: location
    resourceGroupName: managedidentity
  }
}

module createmanagedidentity 'createmanagedidentity.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: containerregistryname
  params: {
    location: location
    miname: managedidentity
  }
}

module createcontainerregistry 'createcontainerregistry.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: containerregistryname
  params: {
    location: location
    subscriptionid: subscriptionid
    resourcegroup: resourcegroup
    containerregistryname: containerregistryname
    managedidentity: managedidentity
  }
}

