param miname string
param location string

resource managedidentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: miname 
  location: location  
  tags: {
    ServiceCode: 'RAC'    
  }
}

resource acrPullRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ManagedIdentityAcrPull'
  properties: {
    roleDefinitionId: '7f951dda-4ed3-4680-a7ca-43fe172d538d'
    principalId: managedidentity_resource.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource acrPushRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ManagedIdentityAcrPush'
  properties: {
    roleDefinitionId: '8311e382-0749-4cb8-b61a-304f252e45ec'
    principalId: managedidentity_resource.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultSecretsOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ManagedIdentitySecretsOfficer'
  properties: {
    roleDefinitionId: 'b86a8fe4-44ce-4948-aee5-eccb2c155cd7'
    principalId: managedidentity_resource.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultSecretsUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ManagedIdentitySecretsUser'
  properties: {
    roleDefinitionId: '4633458b-17de-408a-b874-0445c86b69e6'
    principalId: managedidentity_resource.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource keyVaultCertificatesOfficerRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: 'ManagedIdentityCertificatesOfficer'
  properties: {
    roleDefinitionId: 'a4417e6f-fecd-4de8-b567-7b0420556985'
    principalId: managedidentity_resource.properties.principalId
    principalType: 'ServicePrincipal'
  }
}


resource appConfigDataReaderRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
    name: 'ManagedIdentityConfigDataReader'
    properties: {
    roleDefinitionId: '516239f1-63e1-4d78-a4de-a74fb236a071'
    principalId: managedidentity_resource.properties.principalId
    principalType: 'ServicePrincipal'
    }
}