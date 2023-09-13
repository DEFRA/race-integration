using './createAllContainerApps.bicep'

param resourcegroup = 'DEVRACINFRG1401'
param appConfigURL = ''
param managedidentity = 'DEVRACINFMI1401'
param race2appenvName = 'DEVRACINFAE1401'
param registryResourceGroup = 'DEVRACINFRG1401'
param registryName = 'devracinfcr1401'
param useExternalIngress = true
param containerPort = 80
param frontEndWebServerContainerAppName = 'race2frontendwebserver'
param securityProviderContainerAppName = 'race2securityprovider'
param webApiContainerAppName = 'race2webapi'
param frontendwebservercontainerImage = 'devracinfcr1401.azurecr.io/race2frontendwebserver'
param securityprovidercontainerImage = 'devracinfcr1401.azurecr.io/race2securityprovider'
param webapicontainerImage = 'devracinfcr1401.azurecr.io/race2webapi'
param aspnetCoreEnv = ''
param tag = ''
param minReplicas = 1
param maxReplicas = 2


