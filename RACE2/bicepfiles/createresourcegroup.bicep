// Setting target scope
targetScope = 'subscription'
param resourceGroupName string
param servicePrincipalId string

// Creating resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' existing = {
  name: resourceGroupName
}

resource contributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupContributor'
  properties: {
    roleDefinitionId: 'b24988ac-6180-42a0-ab88-20f7382dd24c'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

output resourceGroupName string = rg.name
