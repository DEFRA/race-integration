// Setting target scope
targetScope = 'subscription'
param resourceGroupName string
param location string
param servicePrincipalId string

// Creating resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourceGroupName
  location: location
}

resource contributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupContributor'
  properties: {
    roleDefinitionId: 'b24988ac-6180-42a0-ab88-20f7382dd24c'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

resource acrPullRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupAcrPull'
  properties: {
    roleDefinitionId: '7f951dda-4ed3-4680-a7ca-43fe172d538d'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

resource acrPushRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupAcrPush'
  properties: {
    roleDefinitionId: '8311e382-0749-4cb8-b61a-304f252e45ec'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

output resourceGroupName string = rg.name