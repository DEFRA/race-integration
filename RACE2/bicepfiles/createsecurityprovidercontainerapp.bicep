param race2appenv string
param registryName string
param registryResourceGroup string
param resourcegroup string
param containerPort int
param managedidentity string 
param appConfigURL string
param aspnetCoreEnv string 
param containerAppName string
param containerImage string
param minReplicas int
param maxReplicas int
param useExternalIngress bool
param tag string
var tagVal=json(tag)
var subscriptionid = subscription().subscriptionId
var location = resourceGroup().location

resource registry 'Microsoft.ContainerRegistry/registries@2022-12-01' existing = {
  name: registryName
  scope: resourceGroup(registryResourceGroup)
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2023-05-01' existing= {
  name: race2appenv 
}

resource managedIdentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing= {
  name: managedidentity 
}

resource containerSecurityProviderApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: containerAppName
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
          env: [
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: aspnetCoreEnv
            }
            {
              name: 'AzureAppConfigURL'
              value: appConfigURL
            }
            {
              name: 'AZURE_CLIENT_ID'
              value: managedIdentity_resource.properties.clientId
            }
          ]          
          image:'${containerImage}:${tagVal.tag}' //concat('${securityprovidercontainerImage}',':','${tagVal.tag}')
          name: containerAppName
        }
      ]
      scale: {
        minReplicas: minReplicas  
        maxReplicas: maxReplicas      
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
