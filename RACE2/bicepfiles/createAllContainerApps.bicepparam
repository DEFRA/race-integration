using './createAllContainerApps.bicep'

param subscriptionid = '97ea02ef-73bc-4ee9-b320-b38a6b41b4d7'
param location = 'uksouth'
param resourcegroup = 'DEVRACINFRG1401'
param managedidentity = 'DEVRACINFMI1401'
param race2appenvName = 'DEVRACINFAE1401'
param registryResourceGroup = 'DEVRACINFRG1401'
param registryName = 'devracinfcr1401'
param useExternalIngress = true
param containerPort = 80
param frontEndContainerAppName = 'race2frontendwebserver'
param securityProviderContainerAppName = 'race2securityprovider'
param webApiContainerAppName = 'race2webapi'
param frontendcontainerImage = 'devracinfcr1401.azurecr.io/race2frontendwebserver'
param securityprovidercontainerImage = 'devracinfcr1401.azurecr.io/race2securityprovider'
param webapicontainerImage = 'devracinfcr1401.azurecr.io/race2webapi'
param tag = ''

