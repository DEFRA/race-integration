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
param minReplicas int
param maxReplicas int

module createfrontendwebservercontainerappmodule 'createfrontendwebservercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerwebserverappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    containerAppName: frontEndWebServerContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: frontendwebservercontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}

module createsecurityprovidercontainerappmodule 'createsecurityprovidercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'securityprovidercontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    containerAppName: securityProviderContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: securityprovidercontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}
module createwebapicontainerappmodule 'createwebapicontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'webapicontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    containerAppName: webApiContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: webapicontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}
