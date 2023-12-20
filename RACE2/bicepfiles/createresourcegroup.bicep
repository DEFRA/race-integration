// Setting target scope
targetScope = 'subscription'
param resourceGroupName string

// Creating resource group
resource rg 'Microsoft.Resources/resourceGroups@2023-07-01' existing = {
  name: resourceGroupName
}

output resourceGroupName string = rg.name
