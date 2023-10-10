using './createSecretsKeysBlobs.bicep' /*TODO: Provide a path to a bicep template*/

param storageAccountName = 'tstracinfst1401'

param sqlServerName = 'tstracinfss1401'

param applicationInsightName = 'TSTRACINFAI1401'

param sqlDatabaseName = 'tstracinfdb1401'

param sqlServerUserName = 'race2admin'

param sqlServerPassword = 'Race2Password123!'

param sqlServerConnectionStringSecretName = 'SqlServerConnString'

param clientSecretName = 'ClientSecret'

param keyVaultName = 'TSTRACINFVT1401'

param appConfigResourceName = 'TSTRACINFAC1401'

param serviceBusResouceName = 'TSTRACINFSB1401'

param storageAccountKeySecretName = 'StorageAccessKey'

param containerName = 's12reporttemplate'

param applicationInsightConnectionStringSecretName = 'AppInsightsConnectionString'

param storageAccountConnectionStringSecretName = 'StorageAccountConnString'

param serviceBusConnectionStringSecretName = 'ServiceBusConnString'

param webserverContainerAppName = 'race2frontendwebserver'

param securityProviderContainerAppName = 'race2securityprovider'

param webApiContainerAppName = 'race2webapi'
