param miname string
param location string = resourceGroup().location

resource managedidentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: miname 
  location: location  
  tags: {
    ServiceCode: 'RAC'    
  }
}
