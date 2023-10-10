using './createSecretsKeysBlobs.bicep' /*TODO: Provide a path to a bicep template*/

param storageAccountName = 'pocracinfst1401'

param sqlServerName = 'pocracinfss1401'

param applicationInsightName = 'POCRACINFAI1401'

param sqlDatabaseName = 'pocracinfdb1401'

param sqlServerUserName = 'race2admin'

param sqlServerPassword = 'Race2Password123!'

param sqlServerConnectionStringSecretName = 'SqlServerConnString'

param clientSecretName = 'ClientSecret'

param keyVaultName = 'POCRACINFVT1401'

param appConfigResourceName = 'POCRACINFAC1401'

param serviceBusResouceName = 'POCRACINFSB1401'

param storageAccountKeySecretName = 'StorageAccessKey'

param containerName = 's12reporttemplate'

param applicationInsightConnectionStringSecretName = 'AppInsightsConnectionString'

param storageAccountConnectionStringSecretName = 'StorageAccountConnString'

param serviceBusConnectionStringSecretName = 'ServiceBusConnString'

param webserverContainerAppName = 'race2frontendwebserver'

param securityProviderContainerAppName = 'race2securityprovider'

param webApiContainerAppName = 'race2webapi'

