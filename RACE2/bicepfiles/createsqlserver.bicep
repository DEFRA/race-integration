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
    administratorLoginPassword: 'Pass123!'
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
