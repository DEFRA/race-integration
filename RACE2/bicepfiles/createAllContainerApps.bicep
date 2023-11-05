param resourcegroup string
param managedidentity string
param race2appenvName string
param securityProviderContainerAppName string
param securityprovidercontainerImage string
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

module createfrontendwebservercontainerappmodule 'createfrontendwebservercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerwebserverappdeploy'
  params: {
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
  }
}

module createsecurityprovidercontainerappmodule 'createsecurityprovidercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'securityprovidercontainerappdeploy'
  params: {
    race2appenv: race2appenvName
    containerAppName: securityProviderContainerAppName
    registryName: registryName
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: securityprovidercontainerImage
    managedidentity: managedidentity
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    tag: tag
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}
module createwebapicontainerappmodule 'createwebapicontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'webapicontainerappdeploy'
  params: {
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
  }
}
module createwebapiexternalcontainerappmodule 'createwebapiexternalcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'webapicontainerappdeploy'
  params: {
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
  }
}
