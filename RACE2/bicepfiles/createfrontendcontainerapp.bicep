param frontEndContainerAppName string
param location string
param race2appenv string
param registryName string
param registryResourceGroup string
param resourcegroup string
param useExternalIngress bool = false
param containerPort int
param frontendcontainerImage string
param managedidentity string
param subscriptionid string 
param appConfigURL string
param aspnetCoreEnv string 
param azureClientId string
param tag string
var tagVal=json(tag)

resource registry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = {
  name: registryName
  scope: resourceGroup(registryResourceGroup)
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2023-08-01-preview' existing= {
  name: race2appenv 
}

resource containerFrontEndApp 'Microsoft.App/containerApps@2022-11-01-preview' = {
  name: frontEndContainerAppName
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
              value: azureClientId
            }
            {
              name: 'ASPNETCORE_FORWARDEDHEADERS_ENABLED'
              value: 'true'
            }
          ]
          image: '${frontendcontainerImage}:${tagVal.tag}' //concat('${frontendcontainerImage}',':','${tagVal.tag}')
          name: frontEndContainerAppName
        }
      ]
      scale: {
        minReplicas: 1  
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
