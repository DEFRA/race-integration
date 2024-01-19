param miname string
param location string

resource managedidentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' = {
  name: miname 
  location: location  
  tags: {
    ServiceCode: 'RAC'    
  }
}

