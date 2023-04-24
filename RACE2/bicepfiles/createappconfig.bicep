param race2appconfigname string
param location string = resourceGroup().location
param subscriptionid string 
param resourcegroup string
param managedidentity string

resource race2appconfig_resource 'Microsoft.AppConfiguration/configurationStores@2022-05-01' = {
  name: race2appconfigname
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
  }
}
