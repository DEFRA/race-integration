// =========== main.bicep ===========

// Setting target scope
targetScope = 'subscription'
param resourceGroupName string
param location string

// Creating resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourceGroupName
  location: location
}

// Deploying storage account using module
module stg 'createstorageaccount.bicep' = {
  name: 'storageDeployment'
  scope: rg    // Deployed in the scope of resource group we created above
  params: {
    storageAccountName: 'stcontoso'
  }
}
