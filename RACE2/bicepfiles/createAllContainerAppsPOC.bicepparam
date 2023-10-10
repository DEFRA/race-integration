using './createAllContainerApps.bicep' /*TODO: Provide a path to a bicep template*/

param resourcegroup = 'Race2ProjectRG'

param managedidentity = 'Race2ManagedIdentity'

param race2appenvName = 'race2containerappenv'

param aspnetCoreEnv = 'Production'

param appConfigURL = 'https://race2appconfig.azconfig.io/'

param registryName = 'race2acr'

param useExternalIngress = true

param containerPort = 80

param minReplicas = 1

param maxReplicas = 2

param frontEndWebServerContainerAppName = 'race2frontendwebserver'

param securityProviderContainerAppName = 'race2securityprovider'

param webApiContainerAppName = 'race2webapi'

param frontendwebservercontainerImage = 'race2acr.azurecr.io/race2frontendwebserver'

param securityprovidercontainerImage = 'race2acr.azurecr.io/race2securityprovider'

param webapicontainerImage = 'race2acr.azurecr.io/race2webapi'

param tag = '351787'

