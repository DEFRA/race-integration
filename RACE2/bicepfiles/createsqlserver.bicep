param location string
param serverName string
param databaseName string
param administratorLogin string
@secure()
param administratorLoginPassword string

resource sqlServer 'Microsoft.Sql/servers@2014-04-01' ={
  name: serverName
  location: location
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
  }
}

resource sqlServerDatabase 'Microsoft.Sql/servers/databases@2014-04-01' = {
  parent: sqlServer
  name: databaseName
  location: location
  
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    edition: 'Standard'
    maxSizeBytes: '34359738368'
    requestedServiceObjectiveName: 'Basic'
  }
}
