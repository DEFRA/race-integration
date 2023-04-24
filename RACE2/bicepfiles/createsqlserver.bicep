@description('The name of the logical server.')
param serverName string

@description('Location for all resources.')
param location string

@description('The name of the Azure AD admin for the SQL server.')
param aad_admin_name string

@description('The Object ID of the Azure AD admin.')
param aad_admin_objectid string

@description('The Tenant ID of the Azure Active Directory')
param aad_admin_tenantid string = subscription().tenantId

@allowed([
  'User'
  'Group'
  'Application'
])
param aad_admin_type string = 'User'
param aad_only_auth bool = true

@description('The Resource ID of the user-assigned managed identity, in the form of /subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>.')
param user_identity_resource_id string = ''

@minLength(1)
param AdminLogin string

@secure()
param AdminLoginPassword string

resource server_resource 'Microsoft.Sql/servers@2020-11-01-preview' = {
  name: serverName
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${user_identity_resource_id}': {}
    }
  }
  properties: {
    administratorLogin: AdminLogin
    administratorLoginPassword: AdminLoginPassword
    primaryUserAssignedIdentityId: user_identity_resource_id
    administrators: {
      login: aad_admin_name
      sid: aad_admin_objectid
      tenantId: aad_admin_tenantid
      principalType: aad_admin_type
      azureADOnlyAuthentication: aad_only_auth
    }
  }
}
