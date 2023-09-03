param storageAccountName string
param serviceBusResouceName string
param sqlServerName string
param sqlDatabaseName string
param sqlServerUserName string
@secure()
param sqlServerPassword string
param keyVaultName string
param appConfigResourceName string
param storageAccountConnectionStringSecretName string
param serviceBusConnectionStringSecretName string
param storageAccountKeySecretName string
param sqlServerConnectionStringSecretName string

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

// Store the connection string in KV if specified
resource storageAccountConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  name: '${keyVault.name}/${storageAccountConnectionStringSecretName}'
  properties: {
    value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};AccountKey=${listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value};EndpointSuffix=${environment().suffixes.storage}'
  }
}

resource storageAccountKeyString 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  name: '${keyVault.name}/${storageAccountKeySecretName}'
  properties: {
    value: '${listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value}'
  }
}

var sqlServerConnectionStringVal = 'Server=tcp:${sqlServerName}${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${sqlDatabaseName};Persist Security Info=False;User ID=${sqlServerUserName};Password=${sqlServerPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
resource sqlServerConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  name: '${keyVault.name}/${sqlServerConnectionStringSecretName}'
  properties: {
    value: sqlServerConnectionStringVal
  }
}

resource serviceBusAccount 'Microsoft.Storage/storageAccounts@2023-01-01' existing = {
  name: serviceBusResouceName
}

var serviceBusEndpoint = '${serviceBusAccount.id}/AuthorizationRules/RootManageSharedAccessKey'
resource ServiceBusConnectionString 'Microsoft.KeyVault/vaults/secrets@2021-11-01-preview' = {
  parent: keyVault
  name: '${serviceBusConnectionStringSecretName}'
  properties: {
    value: listKeys(serviceBusEndpoint, serviceBusAccount.apiVersion).primaryConnectionString
  }
}

// to insert Key Value Pairs
resource configStoreKeyValue 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = [for keyValuePair in keyValuePairs: {
  parent: appConfigStore
  name: empty('${keyValuePair.label}') ? '${keyValuePair.key}' :'${keyValuePair.key}$${keyValuePair.label}'					// key
  properties: {
    value: keyValuePair.contentType == 'string' ? keyValuePair.value : keyValuePair.value == 'SqlServerConnectionStringSecretUrl' ? sqlServerConnectionString.properties.secretUriWithVersion : keyValuePair.value == 'StorageAccountConnectionStringSecretUrl' ? storageAccountConnectionString.properties.secretUriWithVersion : keyValuePair.value=='StorageAccountKeySecretUrl' ? storageAccountKeyString.properties.secretUriWithVersion : keyValuePair.value=='ServiceBusConnectionString' ? ServiceBusConnectionString.properties.secretUriWithVersion :'' // value of the key
    contentType: keyValuePair.contentType	// string representing content type of value
    tags: keyValuePair.tags				        // object: Dictionary of tags 
  }
}]
