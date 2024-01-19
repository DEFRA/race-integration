using './createSecretsKeysBlobs.bicep' /*TODO: Provide a path to a bicep template*/

param storageAccountName = 'race2storageaccount'

param applicationInsightName = 'RACE2AppInSights'

param keyVaultName = 'Race2KeyVault'

param notifyAPIKeySecretName = 'NotifyAPIKey'

param notifyAPIKeySecretValue = 'race2frontend-34699fe1-7b6c-49b7-a562-358f403f75f1-921e2564-534c-4b0a-b705-b9ef5047dd45'

param appConfigResourceName = 'Race2AppConfig'

param storageAccountKeySecretName = 'StorageAccessKey'

param applicationInsightConnectionStringSecretName = 'AppInsightsConnectionString'

param storageAccountConnectionStringSecretName = 'StorageAccountConnString'

param webserverContainerAppName = 'race2frontendwebserver'

param securityProviderContainerAppName = 'race2securityprovider'

param webApiContainerAppName = 'race2webapi'
