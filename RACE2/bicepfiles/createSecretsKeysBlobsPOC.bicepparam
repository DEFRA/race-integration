using './createSecretsKeysBlobs.bicep' /*TODO: Provide a path to a bicep template*/

param storageAccountName = 'race2storageaccount'

param sqlServerName = 'race2sqlserver'

param applicationInsightName = 'RACE2AppInSights'

param sqlDatabaseName = 'RACE2DB'

param sqlServerUserName = 'race2admin'

param sqlServerPassword = 'Race2Password123!'

param sqlServerConnectionStringSecretName = 'SqlServerConnString'

param keyVaultName = 'Race2KeyVault'

param appConfigResourceName = 'Race2AppConfig'

param serviceBusResouceName = 'Race2ServiceBus'

param storageAccountKeySecretName = 'StorageAccessKey'

param containerName = 's12reporttemplate'

param applicationInsightConnectionStringSecretName = 'AppInsightsConnectionString'

param storageAccountConnectionStringSecretName = 'StorageAccountConnString'

param serviceBusConnectionStringSecretName = 'ServiceBusConnString'

param webserverContainerAppName = 'race2frontendwebserver'

param securityProviderContainerAppName = 'race2securityprovider'

param webApiContainerAppName = 'race2webapi'
