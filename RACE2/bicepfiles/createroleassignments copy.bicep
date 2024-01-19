// Setting target scope
targetScope = 'resourceGroup'
param managedIdentityName string
// Creating resource group

resource mi 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' existing = {
  name: managedIdentityName
}

resource contributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: 'b24988ac-6180-42a0-ab88-20f7382dd24c'
}

resource contributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, mi.id, contributorRoleDefinition.id)
  properties: {
    roleDefinitionId: contributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource readerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: 'acdd72a7-3385-48ef-bd42-f606fba81ae7'
}

resource readerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, readerRoleDefinition.id)
  properties: {
    roleDefinitionId: readerRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource dNSZoneContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: 'befefa01-2a29-4197-83a8-272ff33ce314'
}

resource dNSZoneContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id,dNSZoneContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: dNSZoneContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource privateDNSZoneContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: 'b12aa53e-6015-4669-85d0-8515ebb3ae7f'
}

resource privateDNSZoneContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id,privateDNSZoneContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: privateDNSZoneContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource acrPullRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '7f951dda-4ed3-4680-a7ca-43fe172d538d'
}

resource acrPullRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id,acrPullRoleDefinition.id)
  properties: {
    roleDefinitionId: acrPullRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource acrPushRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '8311e382-0749-4cb8-b61a-304f252e45ec'
}

resource acrPushRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, acrPushRoleDefinition.id)
  properties: {
    roleDefinitionId: acrPushRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource appConfigurationDataOwnerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '5ae67dd6-50cb-40e7-96ff-dc2bfa4b606b'
}

resource appConfigurationDataOwnerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, appConfigurationDataOwnerRoleDefinition.id)
  properties: {
    roleDefinitionId: appConfigurationDataOwnerRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource appConfigurationDataReaderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '516239f1-63e1-4d78-a4de-a74fb236a071'
}

resource appConfigurationDataReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, appConfigurationDataReaderRoleDefinition.id)
  properties: {
    roleDefinitionId: appConfigurationDataReaderRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultSecretsUserRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '4633458b-17de-408a-b874-0445c86b69e6'
}

resource keyVaultSecretsUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, keyVaultSecretsUserRoleDefinition.id)
  properties: {
    roleDefinitionId: keyVaultSecretsUserRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultSecretsOfficerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: 'b86a8fe4-44ce-4948-aee5-eccb2c155cd7'
}

resource keyVaultSecretsOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, keyVaultSecretsOfficerRoleDefinition.id)
  properties: {
    roleDefinitionId: keyVaultSecretsOfficerRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultCertificatesOfficerRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: 'a4417e6f-fecd-4de8-b567-7b0420556985'
}

resource keyVaultCertificatesOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, keyVaultCertificatesOfficerRoleDefinition.id)
  properties: {
    roleDefinitionId: keyVaultCertificatesOfficerRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource storageBlobDataContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: 'ba92f5b4-2d11-453d-a403-e96b0029c9fe'
}

resource storageBlobDataContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, storageBlobDataContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: storageBlobDataContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource storageFileDataPrivilegedContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '69566ab7-960f-475b-8e7c-b3118f30c6bd'
}

resource storageFileDataPrivilegedContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, storageFileDataPrivilegedContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: storageFileDataPrivilegedContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource storageTableDataContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '0a9a7e1f-b9d0-4cc4-a60d-0319b160aaa3'
}

resource storageTableDataContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, storageTableDataContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: storageTableDataContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource SQLDBContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '9b7fa17d-e63e-47b0-bb0a-15c516ac86ec'
}

resource SQLDBContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, SQLDBContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: SQLDBContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource SQLManagedInstanceContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '4939a1f6-9ae0-4e48-a1e0-f2cbe897382d'
}

resource SQLManagedInstanceContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, SQLManagedInstanceContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: SQLManagedInstanceContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource SQLServerContributorRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-05-01-preview' existing = {
  scope: resourceGroup()
  name: '6d8ee4ec-f05a-4a1d-8b00-a9b17e38b437'
}

resource SQLServerContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name:  guid(resourceGroup().id, mi.id, SQLServerContributorRoleDefinition.id)
  properties: {
    roleDefinitionId: SQLServerContributorRoleDefinition.id
    principalId: mi.properties.principalId
    principalType: 'ServicePrincipal'
  }
}


