using './createSecretsKeysBlobs.bicep' /*TODO: Provide a path to a bicep template*/

param storageAccountName = 'devracinfst1401'

param sqlServerName = 'devracinfss1401'

param applicationInsightName = 'DEVACINFAI1401'

param sqlDatabaseName = 'devracinfdb1401'

param sqlServerUserName = 'race2admin'

param sqlServerPassword = 'Race2Password123!'

param sqlServerConnectionStringSecretName = 'SqlServerConnString'

param keyVaultName = 'DEVRACINFVT1401'

param appConfigResourceName = 'DEVRACINFAC1401'

param serviceBusResouceName = 'DEVRACINFSB1401'

param storageAccountKeySecretName = 'StorageAccessKey'

param containerName = 's12reporttemplate'

param applicationInsightConnectionStringSecretName = 'AppInsightsConnectionString'

param storageAccountConnectionStringSecretName = 'StorageAccountConnString'

param serviceBusConnectionStringSecretName = 'ServiceBusConnString'

param webserverContainerAppName = 'race2frontendwebserver'

param securityProviderContainerAppName = 'race2securityprovider'

param webApiContainerAppName = 'race2webapi'

