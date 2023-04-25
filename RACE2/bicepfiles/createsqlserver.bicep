param servers_race2sqlserver_name string = 'race2sqlserver1'
param location string

resource servers_race2sqlserver_name_RACE2DB 'Microsoft.Sql/servers/databases@2022-08-01-preview' = {
  name: '${servers_race2sqlserver_name}/RACE2DB'
  location: location
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
    maintenanceConfigurationId: '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_Default'
    isLedgerOn: false
    availabilityZone: 'NoPreference'
  }
}

resource servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/advancedThreatProtectionSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Default'
  properties: {
    state: 'Disabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB_CreateIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'CreateIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB_DbParameterization 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'DbParameterization'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB_DefragmentIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'DefragmentIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB_DropIndex 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'DropIndex'
  properties: {
    autoExecuteValue: 'Disabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB_ForceLastGoodPlan 'Microsoft.Sql/servers/databases/advisors@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'ForceLastGoodPlan'
  properties: {
    autoExecuteValue: 'Enabled'
  }
}

resource Microsoft_Sql_servers_databases_auditingPolicies_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/auditingPolicies@2014-04-01' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Default'
  properties: {
    auditingState: 'Disabled'
  }
}

resource Microsoft_Sql_servers_databases_auditingSettings_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/auditingSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: 'd9cce027-07b6-4275-a215-dd8d52b9d469'
  }
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
}

resource Microsoft_Sql_servers_databases_backupShortTermRetentionPolicies_servers_race2sqlserver_name_RACE2DB_default 'Microsoft.Sql/servers/databases/backupShortTermRetentionPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'default'
  properties: {
    retentionDays: 7
    diffBackupIntervalInHours: 12
  }
}

resource Microsoft_Sql_servers_databases_extendedAuditingSettings_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/extendedAuditingSettings@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'default'
  properties: {
    retentionDays: 0
    isAzureMonitorTargetEnabled: false
    state: 'Disabled'
    storageAccountSubscriptionId: 'd9cce027-07b6-4275-a215-dd8d52b9d469'
  }
}

resource Microsoft_Sql_servers_databases_geoBackupPolicies_servers_race2sqlserver_name_RACE2DB_Default 'Microsoft.Sql/servers/databases/geoBackupPolicies@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB_Current 'Microsoft.Sql/servers/databases/ledgerDigestUploads@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Current'
  properties: {}
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
}

resource Microsoft_Sql_servers_databases_transparentDataEncryption_servers_race2sqlserver_name_RACE2DB_Current 'Microsoft.Sql/servers/databases/transparentDataEncryption@2022-08-01-preview' = {
  parent: servers_race2sqlserver_name_RACE2DB
  name: 'Current'
  properties: {
    state: 'Enabled'
  }
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
}

