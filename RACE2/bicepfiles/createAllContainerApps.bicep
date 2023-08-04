param subscriptionid string 
param location string = resourceGroup().location
param resourcegroup string
param managedidentity string
param race2appenvName string
param securityProviderContainerAppName string
param securityprovidercontainerImage string
param webApiContainerAppName string
param webapicontainerImage string
param frontEndContainerAppName string
param frontendcontainerImage string
param registryName string
param registryResourceGroup string
param useExternalIngress bool = false
param containerPort int
param tag string

module createfrontendcontainerappmodule 'createfrontendcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    frontEndContainerAppName: frontEndContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    frontendcontainerImage: frontendcontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    tag: tag
  }
}

module createsecurityprovidercontainerappmodule 'createsecurityprovidercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'securityprovidercontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    securityProviderContainerAppName: securityProviderContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    securityprovidercontainerImage: securityprovidercontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    tag: tag
  }
}
module createwebapicontainerappmodule 'createwebapicontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'webapicontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    webApiContainerAppName: webApiContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    webapicontainerImage: webapicontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    tag: tag
  }
}
