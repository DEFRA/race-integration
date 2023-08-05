param securityProviderContainerAppName string
param location string
param race2appenv string
param registryName string
param registryResourceGroup string
param resourcegroup string
param useExternalIngress bool = false
param containerPort int
param securityprovidercontainerImage string
param managedidentity string
param subscriptionid string 
param tag string
var tagVal=json(tag)

resource registry 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' existing = {
  name: registryName
  scope: resourceGroup(registryResourceGroup)
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2022-10-01' existing= {
  name: race2appenv 
}

resource containerSecurityProviderApp 'Microsoft.App/containerApps@2022-01-01-preview' = {
  name: securityProviderContainerAppName
  location: location
  properties: {
    managedEnvironmentId: managedEnvironments_race2containerappenv_name_resource.id    
    configuration: {     
      secrets: [
        {
          name: 'container-registry-password'
          value: registry.listCredentials().passwords[0].value
        }
      ]
      registries: [
        {
          server: '${registryName}.azurecr.io'
          username: registry.listCredentials().username
          passwordSecretRef: 'container-registry-password'
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
          image: concat('${securityprovidercontainerImage}',':','${tagVal}')
          name: securityProviderContainerAppName
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
