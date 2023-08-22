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
param revisionMode string
param useProbes bool
param minReplicas int
param maxReplicas int

module createfrontendcontainerappmodule 'createcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    containerAppName: frontEndContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: frontendcontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
    revisionMode: revisionMode
    useProbes: useProbes
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}

module createfrontendwebcontainerappmodule 'createcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerwebappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    containerAppName: frontEndWebContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    containerImage: frontendwebcontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
    appConfigURL: appConfigURL
    aspnetCoreEnv: aspnetCoreEnv
    azureClientId: azureClientId
    tag: tag
    revisionMode: revisionMode
    useProbes: useProbes
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}

module createfrontendwebservercontainerappmodule 'createcontainerapp.bicep' = {
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
    revisionMode: revisionMode
    useProbes: useProbes
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}

module createsecurityprovidercontainerappmodule 'createcontainerapp.bicep' = {
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
    revisionMode: revisionMode
    useProbes: useProbes
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}
module createwebapicontainerappmodule 'createcontainerapp.bicep' = {
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
    revisionMode: revisionMode
    useProbes: useProbes
    minReplicas: minReplicas
    maxReplicas: maxReplicas
  }
}
