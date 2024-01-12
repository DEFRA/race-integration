@secure()
param servers_race2sqlserver_name string
param administratorLogin string
@secure()
param administratorLoginPassword string
param servers_race2sqldb_name string
param location string
param vnet string
param subnetsqlserver string
param tenantId string
param adgroupname string
param adgroupsid string

resource virtualNetworkResource 'Microsoft.Network/virtualNetworks@2023-06-01' existing = {
  name: vnet
}

resource subnetsqlserverResource 'Microsoft.Network/virtualNetworks/subnets@2023-06-01' existing= {
  name: subnetsqlserver
}

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
    publicNetworkAccess: 'Disabled'   
    restrictOutboundNetworkAccess: 'Disabled'
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

resource servers_pocracinfss1401_name_ActiveDirectory 'Microsoft.Sql/servers/administrators@2023-05-01-preview' = {
  parent: servers_race2sqlserver_name_resource
  name: 'ActiveDirectory'
  properties: {
    administratorType: 'ActiveDirectory'
    login: adgroupname
    sid: adgroupsid
    tenantId: tenantId
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

resource sqlPrivateEndpoint 'Microsoft.Network/privateEndpoints@2023-06-01' = {
  name: 'PrivateEndpointSqlServer'
  location: location
  properties: {
    subnet: {
      id: '${virtualNetworkResource.id}/subnets/${subnetsqlserverResource.name}'
    }
    privateLinkServiceConnections: [
      {
        name: 'PrivateEndpointSqlServer'
        properties: {
          privateLinkServiceId: servers_race2sqlserver_name_resource.id
          groupIds: [
            'sqlServer'
          ]
        }
      }
    ]
  }
}
