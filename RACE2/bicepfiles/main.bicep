param containerregistryname string 
param location string =resourceGroup().location
param subscriptionid string 
param resourcegroup string
param managedidentity string

module createcontainerregistry 'createcontainerregistry.bicep' = {
  name: 'RACE2ACR1'
  params: {
    location: location
    subscriptionid: subscriptionid
    resourcegroup: resourcegroup
    containerregistryname: containerregistryname
    managedidentity: managedidentity
  }
}

