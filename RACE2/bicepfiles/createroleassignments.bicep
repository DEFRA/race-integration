// Setting target scope
targetScope = 'resourceGroup'
param managedIdentityName string
// Creating resource group

resource mi 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: managedIdentityName
}

resource contributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupContributor'
  properties: {
    roleDefinitionId: 'b24988ac-6180-42a0-ab88-20f7382dd24c'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource readerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupReader'
  properties: {
    roleDefinitionId: 'acdd72a7-3385-48ef-bd42-f606fba81ae7'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource acrPullRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupAcrPull'
  properties: {
    roleDefinitionId: '7f951dda-4ed3-4680-a7ca-43fe172d538d'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource acrPushRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupAcrPush'
  properties: {
    roleDefinitionId: '8311e382-0749-4cb8-b61a-304f252e45ec'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource appConfigurationDataOwnerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupAppConfigurationDataOwner'
  properties: {
    roleDefinitionId: '5ae67dd6-50cb-40e7-96ff-dc2bfa4b606b'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource appConfigurationDataReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupAppConfigurationDataReader'
  properties: {
    roleDefinitionId: '516239f1-63e1-4d78-a4de-a74fb236a071'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultSecretsUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupKeyVaultSecretsUser'
  properties: {
    roleDefinitionId: '4633458b-17de-408a-b874-0445c86b69e6'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultSecretsOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupKeyVaultSecretsOfficer'
  properties: {
    roleDefinitionId: 'b86a8fe4-44ce-4948-aee5-eccb2c155cd7'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultCertificatesOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupKeyVaultCertificatesOfficer'
  properties: {
    roleDefinitionId: 'a4417e6f-fecd-4de8-b567-7b0420556985'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource storageAccountContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupStorageAccountContributor'
  properties: {
    roleDefinitionId: '17d1049b-9a84-46fb-8f53-869881c3d3ab'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

resource storageBlobDataReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ResourceGroupStorageBlobDataReader'
  properties: {
    roleDefinitionId: '2a2b9908-6ea1-4ae2-8e65-a410df84e7d1'
    principalId: mi.id
    principalType: 'ServicePrincipal'
  }
}

