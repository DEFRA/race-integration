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
param frontEndWebContainerAppName string
param frontendwebcontainerImage string
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
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
  }
}

module createfrontendwebcontainerappmodule 'createfrontendwebcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    frontEndWebContainerAppName: frontEndWebContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    frontendwebcontainerImage: frontendwebcontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
  }
}

module createfrontendwebservercontainerappmodule 'createfrontendwebservercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerappdeploy'
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
