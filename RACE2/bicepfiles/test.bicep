@secure()
param vulnerabilityAssessments_Default_storageContainerPath string
param servers_pocracinfss1401_name string = 'pocracinfss1401'
param privateEndpoints_SqlServerPvtEP_externalid string = '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/POCRACINFRG1401/providers/Microsoft.Network/privateEndpoints/SqlServerPvtEP'
param virtualNetworks_POCRACINFVN1401_externalid string = '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/POCRACINFRG1401/providers/Microsoft.Network/virtualNetworks/POCRACINFVN1401'

resource servers_pocracinfss1401_name_resource 'Microsoft.Sql/servers@2023-05-01-preview' = {
  name: servers_pocracinfss1401_name
  location: 'uksouth'
  tags: {
    ServiceCode: 'RAC'
  }
  kind: 'v12.0'
  properties: {
    administratorLogin: 'race2admin'
    version: '12.0'
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    administrators: {
      administratorType: 'ActiveDirectory'
      principalType: 'Group'
      login: 'AG-Azure-RAC-POC1-Race2'
      sid: '87cb0157-11f3-46aa-99ec-a1c4d4ea4c48'
      tenantId: '770a2450-0227-4c62-90c7-4e38537f1102'
      azureADOnlyAuthentication: true
    }
    restrictOutboundNetworkAccess: 'Disabled'
  }
}

resource servers_pocracinfss1401_name_ActiveDirectory 'Microsoft.Sql/servers/administrators@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'ActiveDirectory'
  properties: {
    administratorType: 'ActiveDirectory'
    login: 'AG-Azure-RAC-POC1-Race2'
    sid: '87cb0157-11f3-46aa-99ec-a1c4d4ea4c48'
    tenantId: '770a2450-0227-4c62-90c7-4e38537f1102'
  }
}

resource servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/advancedThreatProtectionSettings@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
}

resource servers_pocracinfss1401_name_CreateIndex 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'CreateIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_pocracinfss1401_name_DbParameterization 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'DbParameterization'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_pocracinfss1401_name_DefragmentIndex 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'DefragmentIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_pocracinfss1401_name_DropIndex 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'DropIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_pocracinfss1401_name_ForceLastGoodPlan 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'ForceLastGoodPlan'
  properties: {
    autoExecuteValue: 'Enabled'
  }
}

resource Microsoft_Sql_servers_auditingPolicies_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/auditingPolicies@2014-04-01' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Default'
  location: 'UK South'
  properties: {
    auditingState: 'Disabled'
  }
}

resource Microsoft_Sql_servers_auditingSettings_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/auditingSettings@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'default'
  properties: {
    retentionDays: 0
    auditActionsAndGroups: []
    isStorageSecondaryKeyInUse: false
    isAzureMonitorTargetEnabled: false
    isManagedIdentityInUse: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
}

resource Microsoft_Sql_servers_azureADOnlyAuthentications_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/azureADOnlyAuthentications@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Default'
  properties: {
    azureADOnlyAuthentication: true
  }
}

resource Microsoft_Sql_servers_connectionPolicies_servers_pocracinfss1401_name_default 'Microsoft.Sql/servers/connectionPolicies@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'default'
  location: 'uksouth'
  properties: {
    connectionType: 'Default'
  }
}

resource servers_pocracinfss1401_name_pocracinfdb1401 'Microsoft.Sql/servers/databases@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'pocracinfdb1401'
  location: 'uksouth'
  sku: {
    name: 'Standard'
    tier: 'Standard'
    capacity: 10
  }
  kind: 'v12.0,user'
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 268435456000
    catalogCollation: 'SQL_Latin1_General_CP1_CI_AS'
    zoneRedundant: false
    readScale: 'Disabled'
    requestedBackupStorageRedundancy: 'Geo'
    maintenanceConfigurationId: '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_Default'
    isLedgerOn: false
    availabilityZone: 'NoPreference'
  }
}

resource servers_pocracinfss1401_name_master_Default 'Microsoft.Sql/servers/databases/advancedThreatProtectionSettings@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Default'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingPolicies_servers_pocracinfss1401_name_master_Default 'Microsoft.Sql/servers/databases/auditingPolicies@2014-04-01' = {
  name: '${servers_pocracinfss1401_name}/master/Default'
  location: 'UK South'
  properties: {
    auditingState: 'Disabled'
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingSettings_servers_pocracinfss1401_name_master_Default 'Microsoft.Sql/servers/databases/auditingSettings@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_extendedAuditingSettings_servers_pocracinfss1401_name_master_Default 'Microsoft.Sql/servers/databases/extendedAuditingSettings@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_geoBackupPolicies_servers_pocracinfss1401_name_master_Default 'Microsoft.Sql/servers/databases/geoBackupPolicies@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Default'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource servers_pocracinfss1401_name_master_Current 'Microsoft.Sql/servers/databases/ledgerDigestUploads@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Current'
  properties: {}
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_securityAlertPolicies_servers_pocracinfss1401_name_master_Default 'Microsoft.Sql/servers/databases/securityAlertPolicies@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Default'
  properties: {
    state: 'Disabled'
    disabledAlerts: [
      ''
    ]
    emailAddresses: [
      ''
    ]
    emailAccountAdmins: false
    retentionDays: 0
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_transparentDataEncryption_servers_pocracinfss1401_name_master_Current 'Microsoft.Sql/servers/databases/transparentDataEncryption@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Current'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_vulnerabilityAssessments_servers_pocracinfss1401_name_master_Default 'Microsoft.Sql/servers/databases/vulnerabilityAssessments@2023-05-01-preview' = {
  name: '${servers_pocracinfss1401_name}/master/Default'
  properties: {
    recurringScans: {
      isEnabled: false
      emailSubscriptionAdmins: true
      emails: []
    }
  }
  dependsOn: [
    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_devOpsAuditingSettings_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/devOpsAuditingSettings@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Default'
  properties: {
    isAzureMonitorTargetEnabled: false
    isManagedIdentityInUse: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
}

resource servers_pocracinfss1401_name_current 'Microsoft.Sql/servers/encryptionProtector@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'current'
  kind: 'servicemanaged'
  properties: {
    serverKeyName: 'ServiceManaged'
    serverKeyType: 'ServiceManaged'
    autoRotationEnabled: false
  }
}

resource Microsoft_Sql_servers_extendedAuditingSettings_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/extendedAuditingSettings@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'default'
  properties: {
    retentionDays: 0
    auditActionsAndGroups: []
    isStorageSecondaryKeyInUse: false
    isAzureMonitorTargetEnabled: false
    isManagedIdentityInUse: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
}

resource servers_pocracinfss1401_name_AD_Access_to_DB 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'AD Access to DB'
}

resource servers_pocracinfss1401_name_Kris_Capgemini_Laptop 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Kris Capgemini Laptop'
}

resource servers_pocracinfss1401_name_Kris_Defra_Laptop 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Kris Defra Laptop'
}

resource servers_pocracinfss1401_name_Kris_Laptop 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Kris Laptop'
}

resource servers_pocracinfss1401_name_Maha_laptop 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Maha laptop'
}

resource servers_pocracinfss1401_name_ServiceManaged 'Microsoft.Sql/servers/keys@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'ServiceManaged'
  kind: 'servicemanaged'
  properties: {
    serverKeyType: 'ServiceManaged'
  }
}

resource servers_pocracinfss1401_name_SqlServerPvtEP_963af61e_b7b3_4e16_b9a5_025d1d6a66e8 'Microsoft.Sql/servers/privateEndpointConnections@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'SqlServerPvtEP-963af61e-b7b3-4e16-b9a5-025d1d6a66e8'
  properties: {
    privateEndpoint: {
      id: privateEndpoints_SqlServerPvtEP_externalid
    }
    privateLinkServiceConnectionState: {
      status: 'Approved'
      description: 'Auto-approved'
    }
  }
}

resource Microsoft_Sql_servers_securityAlertPolicies_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/securityAlertPolicies@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Default'
  properties: {
    state: 'Enabled'
    disabledAlerts: [
      ''
    ]
    emailAddresses: [
      ''
    ]
    emailAccountAdmins: false
    retentionDays: 0
  }
}

resource Microsoft_Sql_servers_sqlVulnerabilityAssessments_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/sqlVulnerabilityAssessments@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
}

resource servers_pocracinfss1401_name_newVnetRule1 'Microsoft.Sql/servers/virtualNetworkRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'newVnetRule1'
  properties: {
    virtualNetworkSubnetId: '${virtualNetworks_POCRACINFVN1401_externalid}/subnets/subnetazfn'
    ignoreMissingVnetServiceEndpoint: false
  }
}

resource servers_pocracinfss1401_name_newVnetRule2 'Microsoft.Sql/servers/virtualNetworkRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'newVnetRule2'
  properties: {
    virtualNetworkSubnetId: '${virtualNetworks_POCRACINFVN1401_externalid}/subnets/subnetcontainerappenv'
    ignoreMissingVnetServiceEndpoint: false
  }
}

resource servers_pocracinfss1401_name_newVnetRule3 'Microsoft.Sql/servers/virtualNetworkRules@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'newVnetRule3'
  properties: {
    virtualNetworkSubnetId: '${virtualNetworks_POCRACINFVN1401_externalid}/subnets/subnetazfnsql'
    ignoreMissingVnetServiceEndpoint: false
  }
}

resource Microsoft_Sql_servers_vulnerabilityAssessments_servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/vulnerabilityAssessments@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_resource
  name: 'Default'
  properties: {
    recurringScans: {
      isEnabled: false
      emailSubscriptionAdmins: true
    }
    storageContainerPath: vulnerabilityAssessments_Default_storageContainerPath
  }
}

resource servers_pocracinfss1401_name_pocracinfdb1401_Default 'Microsoft.Sql/servers/databases/advancedThreatProtectionSettings@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'Default'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource servers_pocracinfss1401_name_pocracinfdb1401_CreateIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'CreateIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource servers_pocracinfss1401_name_pocracinfdb1401_DbParameterization 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'DbParameterization'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource servers_pocracinfss1401_name_pocracinfdb1401_DefragmentIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'DefragmentIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource servers_pocracinfss1401_name_pocracinfdb1401_DropIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'DropIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource servers_pocracinfss1401_name_pocracinfdb1401_ForceLastGoodPlan 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'ForceLastGoodPlan'
  properties: {
    autoExecuteValue: 'Enabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingPolicies_servers_pocracinfss1401_name_pocracinfdb1401_Default 'Microsoft.Sql/servers/databases/auditingPolicies@2014-04-01' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'Default'
  location: 'UK South'
  properties: {
    auditingState: 'Disabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingSettings_servers_pocracinfss1401_name_pocracinfdb1401_Default 'Microsoft.Sql/servers/databases/auditingSettings@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_backupLongTermRetentionPolicies_servers_pocracinfss1401_name_pocracinfdb1401_default 'Microsoft.Sql/servers/databases/backupLongTermRetentionPolicies@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'default'
  properties: {
    weeklyRetention: 'PT0S'
    monthlyRetention: 'PT0S'
    yearlyRetention: 'PT0S'
    weekOfYear: 0
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_backupShortTermRetentionPolicies_servers_pocracinfss1401_name_pocracinfdb1401_default 'Microsoft.Sql/servers/databases/backupShortTermRetentionPolicies@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'default'
  properties: {
    retentionDays: 7
    diffBackupIntervalInHours: 24
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_extendedAuditingSettings_servers_pocracinfss1401_name_pocracinfdb1401_Default 'Microsoft.Sql/servers/databases/extendedAuditingSettings@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_geoBackupPolicies_servers_pocracinfss1401_name_pocracinfdb1401_Default 'Microsoft.Sql/servers/databases/geoBackupPolicies@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource servers_pocracinfss1401_name_pocracinfdb1401_Current 'Microsoft.Sql/servers/databases/ledgerDigestUploads@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'Current'
  properties: {}
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_securityAlertPolicies_servers_pocracinfss1401_name_pocracinfdb1401_Default 'Microsoft.Sql/servers/databases/securityAlertPolicies@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'Default'
  properties: {
    state: 'Disabled'
    disabledAlerts: [
      ''
    ]
    emailAddresses: [
      ''
    ]
    emailAccountAdmins: false
    retentionDays: 0
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_transparentDataEncryption_servers_pocracinfss1401_name_pocracinfdb1401_Current 'Microsoft.Sql/servers/databases/transparentDataEncryption@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'Current'
  properties: {
    state: 'Enabled'
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_vulnerabilityAssessments_servers_pocracinfss1401_name_pocracinfdb1401_Default 'Microsoft.Sql/servers/databases/vulnerabilityAssessments@2023-05-01-preview' = {
  parent: servers_pocracinfss1401_name_pocracinfdb1401
  name: 'Default'
  properties: {
    recurringScans: {
      isEnabled: false
      emailSubscriptionAdmins: true
      emails: []
    }
  }
  dependsOn: [

    servers_pocracinfss1401_name_resource
  ]
}
