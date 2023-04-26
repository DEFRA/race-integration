param containerAppName string
param location string
param race2appenv string
param registryName string
param registryResourceGroup string
param resourcegroup string
param useExternalIngress bool = false
param containerPort int
param containerImage string
param managedidentity string
param subscriptionid string 
param logAnalyticsWorkspaceName string

resource registry 'Microsoft.ContainerRegistry/registries@2021-12-01-preview' existing = {
  name: registryName
  scope: resourceGroup(registryResourceGroup)
}

resource managedidentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: managedidentity 
  scope: resourceGroup(resourcegroup)
}

resource logAnalyticsWorkspace'Microsoft.OperationalInsights/workspaces@2020-03-01-preview' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: any({
    retentionInDays: 365
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2022-10-01' = {
  name: race2appenv
  location: location
  tags: {
    ServiceCode: 'RAC'
  }
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsWorkspace.properties.customerId
        sharedKey: logAnalyticsWorkspace.listKeys().primarySharedKey
      }
    }
    zoneRedundant: false
    customDomainConfiguration: {
    }
  }
}

output id string = managedEnvironments_race2containerappenv_name_resource.id

resource containerApp 'Microsoft.App/containerApps@2022-01-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: managedEnvironments_race2containerappenv_name_resource.id    
    configuration: {     
      registries: [
        {
          server: '${registryName}.azurecr.io'
          identity:  managedidentity_resource.id
        }
      ]
      ingress: {
        external: useExternalIngress
        targetPort: containerPort
      }
    }
    template: {
      containers: [
        {
          image: containerImage
          name: containerAppName
        }
      ]
      scale: {
        minReplicas: 0  
        maxReplicas: 2      
      }
    }
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '/subscriptions/${subscriptionid}/resourcegroups/${resourcegroup}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/${managedidentity}': {
      }
    }
  }
}


