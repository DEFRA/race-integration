// Setting target scope
targetScope = 'subscription'
param resourceGroupName string
param servicePrincipalId string

// Creating resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' existing = {
  name: resourceGroupName
}

output resourceGroupName string = rg.name
