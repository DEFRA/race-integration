param containerAppName string
param location string
param race2appenvName string
param registryName string
param registryResourceGroup string
param resourcegroup string
param useExternalIngress bool = false
param containerPort int
param containerImage string
param managedidentity string
param subscriptionid string 

resource registry 'Microsoft.ContainerRegistry/registries@2021-12-01-preview' existing = {
  name: registryName
  scope: resourceGroup(registryResourceGroup)
}

resource managedidentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: managedidentity
  scope: resourceGroup(registryResourceGroup)
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2022-10-01' existing = {
  name: race2appenvName
  scope: resourceGroup(resourcegroup)
}

resource containerApp 'Microsoft.App/containerApps@2022-01-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: managedEnvironments_race2containerappenv_name_resource.id    
    configuration: {
      registries: [
        {
          server: '${registryName}.azurecr.io'
          identity: managedidentity_resource.id
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

output url string = containerApp.properties.configuration.ingress.fqdn

