param appconfigName string
param location string = resourceGroup().location
param subscriptionid string 
param resourcegroup string
param managedidentity string

resource race2appconfig_resource 'Microsoft.AppConfiguration/configurationStores@2023-03-01' = {
  name: appconfigName
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  sku: {
    name: 'standard'
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/${subscriptionid}/resourcegroups/${resourcegroup}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/${managedidentity}': {
      }
    }
  }
  properties: {
    encryption: {
    }
    disableLocalAuth: false
    softDeleteRetentionInDays: 7
    enablePurgeProtection: false
    publicNetworkAccess: 'Disabled'
  }
}



