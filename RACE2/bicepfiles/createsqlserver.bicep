param servers_race2sqlserver_name string
param administratorLogin string
@secure()
param administratorLoginPassword string
param servers_race2sqldb_name string
param location string
param tenantId string
param adgroupname string
param adgroupsid string

resource servers_race2sqlserver_name_resource 'Microsoft.Sql/servers@2023-05-01-preview' = {
  name: servers_race2sqlserver_name
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    version: '12.0'
    minimalTlsVersion: '1.2' 
    restrictOutboundNetworkAccess: 'Disabled'
    publicNetworkAccess: 'Disabled'
    administrators: {
      administratorType: 'ActiveDirectory'
      principalType: 'Group'
      login: adgroupname
      sid: adgroupsid
      tenantId: tenantId
      azureADOnlyAuthentication: true
    }
  }
}

// Allow Azure Service and Resources to access this server
resource allowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  name: 'AllowAllWindowsAzureIps' // don't change the name
  parent: servers_race2sqlserver_name_resource
  properties: {
    endIpAddress: '0.0.0.0'
    startIpAddress: '0.0.0.0'
  }
}

resource servers_pocracinfss1401_name_Default 'Microsoft.Sql/servers/advancedThreatProtectionSettings@2023-05-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
}

resource servers_race2sqlserver_name_RACE2DB 'Microsoft.Sql/servers/databases@2023-05-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  location: location
  name: servers_race2sqldb_name
  sku: {
    name: 'GP_Gen5'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }
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

