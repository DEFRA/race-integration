param storageAccountName string
param applicationInsightName string
param keyVaultName string
param appConfigResourceName string
param notifyAPIKeySecretName string
@secure()
param notifyAPIKeySecretValue string
param applicationInsightConnectionStringSecretName string
param storageAccountConnectionStringSecretName string
param storageAccountKeySecretName string
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

resource applicationInsight 'Microsoft.Insights/components@2020-02-02' existing = {
  name: applicationInsightName
}

resource storageAccountConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  parent: keyVault
  name: storageAccountConnectionStringSecretName
  properties: {
    value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};AccountKey=${listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value};EndpointSuffix=${environment().suffixes.storage}'
  }
}

// Store the connection string in KV if specified
resource applicationInsightConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  parent: keyVault
  name: applicationInsightConnectionStringSecretName
  properties: {
    value: applicationInsight.properties.ConnectionString
  }
}

resource storageAccountKeyString 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  parent: keyVault
  name: storageAccountKeySecretName
  properties: {
    value: listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value
  }
}

resource notifyAPIKey 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  parent: keyVault
  name: notifyAPIKeySecretName
  properties: {
    value: notifyAPIKeySecretValue
  }
}

resource webserverContainerApp 'Microsoft.App/containerApps@2023-08-01-preview' existing = {
  name: webserverContainerAppName
}

resource securityProviderContainerApp 'Microsoft.App/containerApps@2023-08-01-preview' existing = {
  name: securityProviderContainerAppName
}

resource webApiContainerApp 'Microsoft.App/containerApps@2023-08-01-preview' existing = {
  name: webApiContainerAppName
}

// to insert Key Value Pairs
resource configStoreKeyValue 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = [for keyValuePair in keyValuePairs: {
  parent: appConfigStore
  name: empty('${keyValuePair.label}') ? '${keyValuePair.key}' :'${keyValuePair.key}$${keyValuePair.label}'					// key
  properties: {
    value: keyValuePair.contentType == 'string' && keyValuePair.key !='SupportedUploadFileExtensions' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='TimeToWaitForUpload' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='EmptyFileSizeLimit' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='ClientSecret' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='CleanContainer' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='UnscannedContainer' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='MaliciousContainer' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='MaintModeEndTime' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='IsMaintenanceMode' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='ConfirmSubmissiontoSETemplateId' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='ConfirmSubmissiontoOperatorTemplateId' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='InternalEmailTemplateId' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='RSTEmailAddress' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='InternalEmailAddress' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='ForgetPasswordTemplateId' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.key !='SqlConnectionString' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.value =='SecurityProviderContainerAppNameValue' ? concat('https://',securityProviderContainerApp.properties.configuration.ingress.fqdn) : keyValuePair.contentType == 'string' && keyValuePair.value =='WebApiContainerAppNameValue' ? concat('https://',concat(webApiContainerApp.properties.configuration.ingress.fqdn,'/graphql')) : keyValuePair.contentType == 'string' && keyValuePair.value =='WebserverContainerAppNameValue' ? concat('https://',webserverContainerApp.properties.configuration.ingress.fqdn) : keyValuePair.contentType == 'string' && keyValuePair.value !='StorageAccountValue' ? keyValuePair.value : keyValuePair.contentType == 'string' && keyValuePair.value =='StorageAccountValue' ? storageAccountName : keyValuePair.value == 'NotifyAPIKeyUrl' ? concat('{"uri":"',notifyAPIKey.properties.secretUri,'"}') : keyValuePair.value == 'StorageAccountConnectionStringSecretUrl' ? concat('{"uri":"',storageAccountConnectionString.properties.secretUri,'"}') : keyValuePair.value=='StorageAccountKeySecretUrl' ? concat('{"uri":"',storageAccountKeyString.properties.secretUri,'"}') : keyValuePair.value=='ApplicationInsightConnectionStringSecretUrl' ? concat('{"uri":"',applicationInsightConnectionString.properties.secretUri,'"}') : '' // value of the key
    contentType: keyValuePair.contentType	// string representing content type of value
    tags: keyValuePair.tags				        // object: Dictionary of tags 
  }
}]
