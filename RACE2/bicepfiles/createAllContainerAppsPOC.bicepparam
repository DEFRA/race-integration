using './createAllContainerApps.bicep' /*TODO: Provide a path to a bicep template*/

param subscriptionid = 'd9cce027-07b6-4275-a215-dd8d52b9d469'

param location = 'westeurope'

param resourcegroup = 'Race2ProjectRG'

param managedidentity = 'Race2ManagedIdentity'

param race2appenvName = 'race2containerappenv'

param registryResourceGroup = 'Race2ProjectRG'

param aspnetCoreEnv = 'Production'

param azureClientId = 'f324da0d-19e1-49df-8901-faab72ed2649'

param appConfigURL = 'https://race2appconfig.azconfig.io/'

param registryName = 'race2acr'

param useExternalIngress = true

param containerPort = 80

param revisionMode = 'Single'

param useProbes = true

param minReplicas = 1

param maxReplicas = 2

param frontEndWebServerContainerAppName = 'race2frontendwebserver'

param securityProviderContainerAppName = 'race2securityprovider'

param webApiContainerAppName = 'race2webapi'

param frontendwebservercontainerImage = 'race2acr.azurecr.io/race2frontendwebserver'

param securityprovidercontainerImage = 'race2acr.azurecr.io/race2securityprovider'

param webapicontainerImage = 'race2acr.azurecr.io/race2webapi'

param tag = '351787'

