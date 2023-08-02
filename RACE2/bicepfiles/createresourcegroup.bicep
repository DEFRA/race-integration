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

resource kvCeritificateOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupKeyVaultCertificateOfficer'
  properties: {
    roleDefinitionId: 'a4417e6f-fecd-4de8-b567-7b0420556985'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

resource kvSecrestOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupKeyVaultSecretsOfficer'
  properties: {
    roleDefinitionId: 'b86a8fe4-44ce-4948-aee5-eccb2c155cd7'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

resource kvSecretsUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupKeyVaultSecretsUser'
  properties: {
    roleDefinitionId: '4633458b-17de-408a-b874-0445c86b69e6'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

resource appConfigDataReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupAppConfigDataReader'
  properties: {
    roleDefinitionId: '516239f1-63e1-4d78-a4de-a74fb236a071'
    principalId: servicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

output resourceGroupName string = rg.name
