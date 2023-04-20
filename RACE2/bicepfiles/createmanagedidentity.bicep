param miname string = 'Race2ManagedIdentity'
param loc string = 'westeurope' 

resource managedidentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: miname 
  location: loc  
  tags: {
    ServiceCode: 'RAC'    
  }
}
