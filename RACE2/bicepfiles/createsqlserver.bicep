@secure()
param servers_race2sqlserver_name string
param location string

resource servers_race2sqlserver_name_resource 'Microsoft.Sql/servers@2022-08-01-preview' = {
  name: servers_race2sqlserver_name
  location: location
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
      login: 'Kriss.Sahoo@defra.gov.uk'
      sid: '160b1ec3-b830-4c1c-8e01-9039cf27364a'
      tenantId: '770a2450-0227-4c62-90c7-4e38537f1102'
      azureADOnlyAuthentication: false
    }
    restrictOutboundNetworkAccess: 'Disabled'
  }
}

resource servers_race2sqlserver_name_ActiveDirectory 'Microsoft.Sql/servers/administrators@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'ActiveDirectory'
  properties: {
    administratorType: 'ActiveDirectory'
    login: 'Kriss.Sahoo@defra.gov.uk'
    sid: '160b1ec3-b830-4c1c-8e01-9039cf27364a'
    tenantId: '770a2450-0227-4c62-90c7-4e38537f1102'
  }
}

resource servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/advancedThreatProtectionSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
}

resource servers_race2sqlserver_name_CreateIndex 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_resource
  name: 'CreateIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_DbParameterization 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_resource
  name: 'DbParameterization'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_DefragmentIndex 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_resource
  name: 'DefragmentIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_DropIndex 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_resource
  name: 'DropIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_ForceLastGoodPlan 'Microsoft.Sql/servers/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_resource
  name: 'ForceLastGoodPlan'
  properties: {
    autoExecuteValue: 'Enabled'
  }
}

resource Microsoft_Sql_servers_auditingPolicies_servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/auditingPolicies@2014-04-01' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Default'
  properties: {
    auditingState: 'Disabled'
  }
}

resource Microsoft_Sql_servers_auditingSettings_servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/auditingSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
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

resource Microsoft_Sql_servers_azureADOnlyAuthentications_servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/azureADOnlyAuthentications@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Default'
  properties: {
    azureADOnlyAuthentication: false
  }
}

resource Microsoft_Sql_servers_connectionPolicies_servers_race2sqlserver_name_default 'Microsoft.Sql/servers/connectionPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'default'
  properties: {
    connectionType: 'Default'
  }
}

resource servers_race2sqlserver_name_RACE2DB 'Microsoft.Sql/servers/databases@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  location: location
  name: 'RACE2DB'
  sku: {
    name: 'GP_Gen5'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }
  kind: 'v12.0,user,vcore'
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 34359738368
    catalogCollation: 'SQL_Latin1_General_CP1_CI_AS'
    zoneRedundant: false
    licenseType: 'LicenseIncluded'
    readScale: 'Disabled'
    requestedBackupStorageRedundancy: 'Geo'
    isLedgerOn: false
    availabilityZone: 'NoPreference'
  }
}

resource servers_race2sqlserver_name_master_Default 'Microsoft.Sql/servers/databases/advancedThreatProtectionSettings@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Default'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingPolicies_servers_race2sqlserver_name_master_Default 'Microsoft.Sql/servers/databases/auditingPolicies@2014-04-01' = {
  name: '${servers_race2sqlserver_name}/master/Default'
  properties: {
    auditingState: 'Disabled'
  }
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingSettings_servers_race2sqlserver_name_master_Default 'Microsoft.Sql/servers/databases/auditingSettings@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: 'd9cce027-07b6-4275-a215-dd8d52b9d469'
  }
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_extendedAuditingSettings_servers_race2sqlserver_name_master_Default 'Microsoft.Sql/servers/databases/extendedAuditingSettings@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: 'd9cce027-07b6-4275-a215-dd8d52b9d469'
  }
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_geoBackupPolicies_servers_race2sqlserver_name_master_Default 'Microsoft.Sql/servers/databases/geoBackupPolicies@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Default'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource servers_race2sqlserver_name_master_Current 'Microsoft.Sql/servers/databases/ledgerDigestUploads@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Current'
  properties: {}
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_securityAlertPolicies_servers_race2sqlserver_name_master_Default 'Microsoft.Sql/servers/databases/securityAlertPolicies@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Default'
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
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_transparentDataEncryption_servers_race2sqlserver_name_master_Current 'Microsoft.Sql/servers/databases/transparentDataEncryption@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Current'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_vulnerabilityAssessments_servers_race2sqlserver_name_master_Default 'Microsoft.Sql/servers/databases/vulnerabilityAssessments@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/master/Default'
  properties: {
    recurringScans: {
      isEnabled: false
      emailSubscriptionAdmins: true
      emails: []
    }
  }
  dependsOn: [
    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_devOpsAuditingSettings_servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/devOpsAuditingSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Default'
  properties: {
    isAzureMonitorTargetEnabled: false
    isManagedIdentityInUse: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
}

resource servers_race2sqlserver_name_current 'Microsoft.Sql/servers/encryptionProtector@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'current'
  kind: 'servicemanaged'
  properties: {
    serverKeyName: 'ServiceManaged'
    serverKeyType: 'ServiceManaged'
    autoRotationEnabled: false
  }
}

resource Microsoft_Sql_servers_extendedAuditingSettings_servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/extendedAuditingSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
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

resource servers_race2sqlserver_name_AllowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'AllowAllWindowsAzureIps'
}

resource servers_race2sqlserver_name_ClientIPAddress_2023_04_24_02_47_00 'Microsoft.Sql/servers/firewallRules@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'ClientIPAddress_2023-04-24_02:47:00'
}

resource servers_race2sqlserver_name_Kris_Capgemini_Laptop 'Microsoft.Sql/servers/firewallRules@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Kris Capgemini Laptop'
}

resource servers_race2sqlserver_name_Maha_Capgemini_Laptop 'Microsoft.Sql/servers/firewallRules@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Maha Capgemini Laptop'
}

resource servers_race2sqlserver_name_ServiceManaged 'Microsoft.Sql/servers/keys@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'ServiceManaged'
  kind: 'servicemanaged'
  properties: {
    serverKeyType: 'ServiceManaged'
  }
}

resource Microsoft_Sql_servers_securityAlertPolicies_servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/securityAlertPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
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

resource Microsoft_Sql_servers_sqlVulnerabilityAssessments_servers_race2sqlserver_name_Default 'Microsoft.Sql/servers/sqlVulnerabilityAssessments@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/advancedThreatProtectionSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Default'
  properties: {
    state: 'Disabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource servers_race2sqlserver_name_RACE2DB_CreateIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'CreateIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource servers_race2sqlserver_name_RACE2DB_DbParameterization 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'DbParameterization'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource servers_race2sqlserver_name_RACE2DB_DefragmentIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'DefragmentIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource servers_race2sqlserver_name_RACE2DB_DropIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'DropIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource servers_race2sqlserver_name_RACE2DB_ForceLastGoodPlan 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'ForceLastGoodPlan'
  properties: {
    autoExecuteValue: 'Enabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingPolicies_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/auditingPolicies@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Default'
  properties: {
    auditingState: 'Disabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_auditingSettings_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/auditingSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_backupLongTermRetentionPolicies_servers_race2sqlserver_name_RACE2DB_default 'Microsoft.Sql/servers/databases/backupLongTermRetentionPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'default'
  properties: {
    weeklyRetention: 'PT0S'
    monthlyRetention: 'PT0S'
    yearlyRetention: 'PT0S'
    weekOfYear: 0
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_backupShortTermRetentionPolicies_servers_race2sqlserver_name_RACE2DB_default 'Microsoft.Sql/servers/databases/backupShortTermRetentionPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'default'
  properties: {
    retentionDays: 7
    diffBackupIntervalInHours: 12
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_extendedAuditingSettings_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/extendedAuditingSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: '00000000-0000-0000-0000-000000000000'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_geoBackupPolicies_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/geoBackupPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource servers_race2sqlserver_name_RACE2DB_Current 'Microsoft.Sql/servers/databases/ledgerDigestUploads@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Current'
  properties: {}
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_securityAlertPolicies_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/securityAlertPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
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

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_transparentDataEncryption_servers_race2sqlserver_name_RACE2DB_Current 'Microsoft.Sql/servers/databases/transparentDataEncryption@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Current'
  properties: {
    state: 'Enabled'
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}

resource Microsoft_Sql_servers_databases_vulnerabilityAssessments_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/vulnerabilityAssessments@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Default'
  properties: {
    recurringScans: {
      isEnabled: false
      emailSubscriptionAdmins: true
      emails: []
    }
  }
  dependsOn: [

    servers_race2sqlserver_name_resource
  ]
}
