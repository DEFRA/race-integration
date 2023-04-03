param storageAccounts_race2storageaccount_name string = 'race2storageaccount'
param loc string = 'westeurope'

resource storageAccounts_race2storageaccount_name_resource 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: storageAccounts_race2storageaccount_name
  location: loc
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
      bypass: 'AzureServices'
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

resource storageAccounts_race2storageaccount_name_default 'Microsoft.Storage/storageAccounts/blobServices@2022-09-01' = {
  parent: storageAccounts_race2storageaccount_name_resource
  name: 'default'
  sku: {
    name: 'Standard_ZRS'
    tier: 'Standard'
  }
  properties: {
    changeFeed: {
      enabled: false
    }
    restorePolicy: {
      enabled: false
    }
    containerDeleteRetentionPolicy: {
      enabled: true
      days: 7
    }
    cors: {
      corsRules: []
    }
    deleteRetentionPolicy: {
      allowPermanentDelete: false
      enabled: true
      days: 7
    }
    isVersioningEnabled: false
  }
}

resource Microsoft_Storage_storageAccounts_fileServices_storageAccounts_race2storageaccount_name_default 'Microsoft.Storage/storageAccounts/fileServices@2022-09-01' = {
  parent: storageAccounts_race2storageaccount_name_resource
  name: 'default'
  sku: {
    name: 'Standard_ZRS'
    tier: 'Standard'
  }
  properties: {
    protocolSettings: {
      smb: {
      }
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

resource Microsoft_Storage_storageAccounts_queueServices_storageAccounts_race2storageaccount_name_default 'Microsoft.Storage/storageAccounts/queueServices@2022-09-01' = {
  parent: storageAccounts_race2storageaccount_name_resource
  name: 'default'
  properties: {
    cors: {
      corsRules: []
    }
  }
}

resource Microsoft_Storage_storageAccounts_tableServices_storageAccounts_race2storageaccount_name_default 'Microsoft.Storage/storageAccounts/tableServices@2022-09-01' = {
  parent: storageAccounts_race2storageaccount_name_resource
  name: 'default'
  properties: {
    cors: {
      corsRules: []
    }
  }
}

resource storageAccounts_race2storageaccount_name_default_race2appauditlog 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-09-01' = {
  parent: storageAccounts_race2storageaccount_name_default
  name: 'race2appauditlog'
  properties: {
    immutableStorageWithVersioning: {
      enabled: false
    }
    defaultEncryptionScope: '$account-encryption-key'
    denyEncryptionScopeOverride: false
    publicAccess: 'None'
  }
  dependsOn: [
    storageAccounts_race2storageaccount_name_resource
  ]
}

resource Microsoft_Storage_storageAccounts_fileServices_shares_storageAccounts_race2storageaccount_name_default_race2appauditlog 'Microsoft.Storage/storageAccounts/fileServices/shares@2022-09-01' = {
  parent: Microsoft_Storage_storageAccounts_fileServices_storageAccounts_race2storageaccount_name_default
  name: 'race2appauditlog'
  properties: {
    accessTier: 'TransactionOptimized'
    shareQuota: 5120
    enabledProtocols: 'SMB'
  }
  dependsOn: [
    storageAccounts_race2storageaccount_name_resource
  ]
}
