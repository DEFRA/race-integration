// Setting target scope
targetScope = 'subscription'

param resourceGroupName string
param location string
param keyvaultName string

// Creating resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourceGroupName
  location: location
}

resource keyVault 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  name: keyvaultName
  location: location
  properties:{
    scope: rg
    enabledForTemplateDeployment: true
    enableRbacAuthorization: true
    tenantId: subscription().tenantId
    sku: {
      family: 'A'
      name: 'standard'
    }
  }
}  
