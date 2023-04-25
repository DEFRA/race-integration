param keyvaultName string
param location string = resourceGroup().location

resource Race2KeyVault_resource 'Microsoft.KeyVault/vaults@2022-11-01' = {
    name: keyvaultName
    location: location

    properties:{
      enabledForTemplateDeployment: true
      enableRbacAuthorization: true
      tenantId: subscription().tenantId
      sku: {
        family: 'A'
        name: 'standard'
      }
    }
} 

  