param subscriptionid string 
param location string = resourceGroup().location
param resourcegroup string
param managedidentity string
param race2appenvName string
param securityProviderContainerAppName string
param securityprovidercontainerImage string
param webApiContainerAppName string
param webapicontainerImage string
param frontEndWebServerContainerAppName string
param frontendwebservercontainerImage string
param registryName string
param registryResourceGroup string
param useExternalIngress bool = false
param containerPort int
param appConfigURL string
param aspnetCoreEnv string 
param azureClientId string
param tag string

module createfrontendwebservercontainerappmodule 'createfrontendwebservercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerwebserverappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    frontEndWebServerContainerAppName: frontEndWebServerContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    frontendwebservercontainerImage: frontendwebservercontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
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
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
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
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
  }
}
