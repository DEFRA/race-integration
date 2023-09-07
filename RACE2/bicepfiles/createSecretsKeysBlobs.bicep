param storageAccountName string
param serviceBusResouceName string
param sqlServerName string
param sqlDatabaseName string
param sqlServerUserName string
param applicationInsightName string
@secure()
param sqlServerPassword string
param keyVaultName string
param appConfigResourceName string
param applicationInsightConnectionStringSecretName string
param storageAccountConnectionStringSecretName string
param serviceBusConnectionStringSecretName string
param storageAccountKeySecretName string
param sqlServerConnectionStringSecretName string
param containerName string
param webserverContainerAppName string
param securityProviderContainerAppName string
param webApiContainerAppName string

// Load pairs from file
var keyValuePairs = loadJsonContent('key-value-pairs.json')
// Get reference to KV
resource keyVault 'Microsoft.KeyVault/vaults@2023-02-01' existing = {
  name: keyVaultName
}

resource appConfigStore  'Microsoft.AppConfiguration/configurationStores@2023-03-01' existing = {
  name: appConfigResourceName
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' existing = {
  name: storageAccountName
}

resource blobService 'Microsoft.Storage/storageAccounts/blobServices@2021-06-01' existing= {
  name: '${storageAccount.name}/default'
}

resource applicationInsight 'Microsoft.Insights/components@2020-02-02' existing = {
  name: applicationInsightName
}

resource serviceBusAccount 'Microsoft.ServiceBus/namespaces@2022-10-01-preview' existing = {
  name: serviceBusResouceName
}

resource storageAccountConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: storageAccountConnectionStringSecretName
  properties: {
    value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};AccountKey=${listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value};EndpointSuffix=${environment().suffixes.storage}'
  }
}

resource container 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-01-01' =  {
  parent: blobService
  name: toLower(containerName)
  properties: {
    publicAccess: 'None'
    metadata: {}
  }
}

// Store the connection string in KV if specified
resource applicationInsightConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: applicationInsightConnectionStringSecretName
  properties: {
    value: applicationInsight.properties.ConnectionString
  }
}

resource storageAccountKeyString 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: storageAccountKeySecretName
  properties: {
    value: listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value
  }
}

var sqlServerConnectionStringVal = 'Server=tcp:${sqlServerName}${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${sqlDatabaseName};Persist Security Info=False;User ID=${sqlServerUserName};Password=${sqlServerPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
resource sqlServerConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: sqlServerConnectionStringSecretName
  properties: {
    value: sqlServerConnectionStringVal
  }
}

var serviceBusEndpoint = '${serviceBusAccount.id}/AuthorizationRules/RootManageSharedAccessKey'
resource serviceBusConnectionString 'Microsoft.KeyVault/vaults/secrets@2021-11-01-preview' = {
  parent: keyVault
  name: serviceBusConnectionStringSecretName
  properties: {
    value: listKeys(serviceBusEndpoint, serviceBusAccount.apiVersion).primaryConnectionString
  }
}

resource webserverContainerApp 'Microsoft.App/containerApps@2023-05-01' existing = {
  name: webserverContainerAppName
}

resource securityProviderContainerApp 'Microsoft.App/containerApps@2023-05-01' existing = {
  name: securityProviderContainerAppName
}

resource webApiContainerApp 'Microsoft.App/containerApps@2023-05-01' existing = {
  name: webApiContainerAppName
}

// to insert Key Value Pairs
resource configStoreKeyValue 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = [for keyValuePair in keyValuePairs: {
  parent: appConfigStore
  name: empty('${keyValuePair.label}') ? '${keyValuePair.key}' :'${keyValuePair.key}$${keyValuePair.label}'					// key
  properties: {
    value: keyValuePair.contentType == 'string' && keyValuePair.value !='SecurityProviderContainerAppNameValue' ? securityProviderContainerApp.properties.configuration.ingress.fqdn : keyValuePair.contentType == 'string' && keyValuePair.value !='WebApiContainerAppNameValue' ? webApiContainerApp.properties.configuration.ingress.fqdn : keyValuePair.contentType == 'string' && keyValuePair.value !='WebserverContainerAppNameValue' ? webserverContainerApp.properties.configuration.ingress.fqdn : keyValuePair.contentType == 'string' && keyValuePair.value !='StorageAccountValue' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.value =='StorageAccountValue' ? storageAccountName : keyValuePair.value == 'SqlServerConnectionStringSecretUrl' ? concat('{"uri":"',sqlServerConnectionString.properties.secretUri,'"}') : keyValuePair.value == 'StorageAccountConnectionStringSecretUrl' ? concat('{"uri":"',storageAccountConnectionString.properties.secretUri,'"}') : keyValuePair.value=='StorageAccountKeySecretUrl' ? concat('{"uri":"',storageAccountKeyString.properties.secretUri,'"}') : keyValuePair.value=='ServiceBusConnectionStringSecretUrl' ? concat('{"uri":"',serviceBusConnectionString.properties.secretUri,'"}') : keyValuePair.value=='ApplicationInsightConnectionStringSecretUrl' ? concat('{"uri":"',applicationInsightConnectionString.properties.secretUri,'"}') : '' // value of the key
    contentType: keyValuePair.contentType	// string representing content type of value
    tags: keyValuePair.tags				        // object: Dictionary of tags 
  }
}]
