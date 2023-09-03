param storageAccountName string
param keyVaultName string
param appConfigResourceName string
param connectionStringSecretName string

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
  name: '${keyVault.name}/${connectionStringSecretName}'
  properties: {
    value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};AccountKey=${listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value};EndpointSuffix=${environment().suffixes.storage}'
  }
}

// to insert Key Value Pairs
resource configStoreKeyValue 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = [for keyValuePair in keyValuePairs: {
  parent: appConfigStore
  name: keyValuePair.key					// key
  properties: {
    value: keyValuePair.value				// value of the key
    contentType: keyValuePair.contentType	// string representing content type of value
    tags: keyValuePair.tags					// object: Dictionary of tags 
    label: keyValuePair.label
  }
}]
