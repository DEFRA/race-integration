// =========== storage.bicep ===========

// targetScope = 'resourceGroup' - not needed since it is the default value

resource stg 'Microsoft.Storage/storageAccounts@2021-02-01' = {
  name: 'rcstg'
  location: resourceGroup().location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}
