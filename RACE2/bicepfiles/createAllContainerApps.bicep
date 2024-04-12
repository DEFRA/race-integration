param azureTenanatId string
param location string
param resourcegroup string
param managedidentity string
param race2appenvName string
param webApiContainerAppName string
param webapicontainerImage string
param webApiExternalContainerAppName string
param webapiexternalcontainerImage string
param frontEndWebServerContainerAppName string
param frontendwebservercontainerImage string
param registryName string
param useExternalIngress bool = false
param containerPort int
param appConfigURL string
param aspnetCoreEnv string 
param tag string
param minReplicas int
param maxReplicas int
param transport string
param allowInsecure bool

module createfrontendwebservercontainerappmodule 'createfrontendwebservercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerwebserverappdeploy'
  params: {
    azureTenanatId: azureTenanatId
    location: location
    race2appenv: race2appenvName
    containerAppName: frontEndWebServerContainerAppName
    registryName: registryName
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: frontendwebservercontainerImage
    managedidentity: managedidentity
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    tag: tag
    minReplicas: minReplicas
    maxReplicas: maxReplicas
    transport: transport
    allowInsecure: allowInsecure
  }
}

module createwebapicontainerappmodule 'createwebapicontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'webapicontainerappdeploy'
  params: {
    azureTenanatId: azureTenanatId
    location: location
    race2appenv: race2appenvName
    containerAppName: webApiContainerAppName
    registryName: registryName
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: webapicontainerImage
    managedidentity: managedidentity
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    tag: tag
    minReplicas: minReplicas
    maxReplicas: maxReplicas
    transport: transport
    allowInsecure: allowInsecure
  }
}
module createwebapiexternalcontainerappmodule 'createwebapiexternalcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'webapiexternalcontainerappdeploy'
  params: {
    azureTenanatId: azureTenanatId
    location: location
    race2appenv: race2appenvName
    containerAppName: webApiExternalContainerAppName
    registryName: registryName
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: webapiexternalcontainerImage
    managedidentity: managedidentity
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    tag: tag
    minReplicas: minReplicas
    maxReplicas: maxReplicas
    transport: transport
    allowInsecure: allowInsecure
  }
}
