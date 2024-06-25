param azureTenanatId string
param location string
param resourcegroup string
param managedidentity string
param race2appenvName string
param backendapiintegrationContainerAppName string
param backendapiintegrationcontainerImage string
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

module createbackendapiintegrationcontainerappmodule 'createbackendapiintegrationcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'backendapiintegrationcontainerappdeploy'
  params: {
    azureTenanatId: azureTenanatId
    location: location
    race2appenv: race2appenvName
    containerAppName: backendapiintegrationContainerAppName
    registryName: registryName
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: backendapiintegrationcontainerImage
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
