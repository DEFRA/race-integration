param storageAccounts_prdracinfst1401_name string = 'prdracinfst1401'

resource storageAccounts_prdracinfst1401_name_resource 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccounts_prdracinfst1401_name
  location: 'uksouth'
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'Standard_ZRS'
    tier: 'Standard'
  }
  kind: 'StorageV2'
  properties: {
    dnsEndpointType: 'Standard'
    defaultToOAuthAuthentication: false
    publicNetworkAccess: 'Enabled'
    allowCrossTenantReplication: true
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: true
    allowSharedKeyAccess: true
    networkAcls: {
      resourceAccessRules: []
      bypass: 'None'
      virtualNetworkRules: []
      ipRules: []
      defaultAction: 'Allow'
    }
    supportsHttpsTrafficOnly: true
    encryption: {
      requireInfrastructureEncryption: false
      services: {
        file: {
          keyType: 'Account'
          enabled: true
        }
        blob: {
          keyType: 'Account'
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }
    accessTier: 'Hot'
  }
}

resource storageAccounts_prdracinfst1401_name_default 'Microsoft.Storage/storageAccounts/blobServices@2023-01-01' = {
  parent: storageAccounts_prdracinfst1401_name_resource
  name: 'default'
  sku: {
    name: 'Standard_ZRS'
    tier: 'Standard'
  }
  properties: {
    cors: {
      corsRules: []
    }
    deleteRetentionPolicy: {
      allowPermanentDelete: false
      enabled: false
    }
  }
}

resource Microsoft_Storage_storageAccounts_fileServices_storageAccounts_prdracinfst1401_name_default 'Microsoft.Storage/storageAccounts/fileServices@2023-01-01' = {
  parent: storageAccounts_prdracinfst1401_name_resource
  name: 'default'
  sku: {
    name: 'Standard_ZRS'
    tier: 'Standard'
  }
  properties: {
    protocolSettings: {
      smb: {}
    }
    cors: {
      corsRules: []
    }
    shareDeleteRetentionPolicy: {
      enabled: true
      days: 7
    }
  }
}

resource storageAccounts_prdracinfst1401_name_storageAccounts_prdracinfst1401_name_11266c55_3e01_4596_847a_d0af14a3d16d 'Microsoft.Storage/storageAccounts/privateEndpointConnections@2023-01-01' = {
  parent: storageAccounts_prdracinfst1401_name_resource
  name: '${storageAccounts_prdracinfst1401_name}.11266c55-3e01-4596-847a-d0af14a3d16d'
  properties: {
    provisioningState: 'Succeeded'
    privateEndpoint: {}
    privateLinkServiceConnectionState: {
      status: 'Approved'
      description: 'Auto-Approved'
      actionRequired: 'None'
    }
  }
}

resource Microsoft_Storage_storageAccounts_queueServices_storageAccounts_prdracinfst1401_name_default 'Microsoft.Storage/storageAccounts/queueServices@2023-01-01' = {
  parent: storageAccounts_prdracinfst1401_name_resource
  name: 'default'
  properties: {
    cors: {
      corsRules: []
    }
  }
}

resource Microsoft_Storage_storageAccounts_tableServices_storageAccounts_prdracinfst1401_name_default 'Microsoft.Storage/storageAccounts/tableServices@2023-01-01' = {
  parent: storageAccounts_prdracinfst1401_name_resource
  name: 'default'
  properties: {
    cors: {
      corsRules: []
    }
  }
}
