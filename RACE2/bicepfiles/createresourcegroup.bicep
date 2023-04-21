// Setting target scope
targetScope = 'subscription'
param resourceGroupName string
param location string

// Creating resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourceGroupName
  location: location
}

