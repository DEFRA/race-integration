param keyvaultName string
param location string
param tenantId string

resource Race2KeyVault_resource 'Microsoft.KeyVault/vaults@2022-11-01' = {
  name: keyvaultName
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: tenantId
    enabledForDeployment: true
    enabledForDiskEncryption: true
    enabledForTemplateDeployment: true
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    enableRbacAuthorization: true
    vaultUri: 'https://${keyvaultName}.vault.azure.net/'
    provisioningState: 'Succeeded'
    publicNetworkAccess: 'Enabled'
  }
}

resource vaults_Race2KeyVault_name_ADO_DefraGovUK_AZD_RAC_POC1 'Microsoft.KeyVault/vaults/secrets@2022-11-01' = {
  parent: Race2KeyVault_resource
  name: 'ADO-DefraGovUK-AZD-RAC-POC1'
  properties: {
    attributes: {
      enabled: true
      exp: 1741951481
    }
  }
}

resource vaults_Race2KeyVault_name_SqlServerConnectionDev 'Microsoft.KeyVault/vaults/secrets@2022-11-01' = {
  parent: Race2KeyVault_resource
  name: 'SqlServerConnectionDev'
  properties: {
    attributes: {
      enabled: true
    }
  }
}

resource vaults_Race2KeyVault_name_SqlServerConnString 'Microsoft.KeyVault/vaults/secrets@2022-11-01' = {
  parent: Race2KeyVault_resource
  name: 'SqlServerConnString'
  properties: {
    attributes: {
      enabled: true
    }
  }
}
