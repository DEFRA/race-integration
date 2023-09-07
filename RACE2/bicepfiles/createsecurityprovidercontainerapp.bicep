param location string
param race2appenv string
param registryName string
param registryResourceGroup string
param resourcegroup string
param useExternalIngress bool = false
param containerPort int
param managedidentity string
param subscriptionid string 
param appConfigURL string
param aspnetCoreEnv string 
param azureClientId string
param containerAppName string
param containerImage string
param revisionMode string
param useProbes bool
param minReplicas int
param maxReplicas int
param tag string
var tagVal=json(tag)

resource registry 'Microsoft.ContainerRegistry/registries@2022-12-01' existing = {
  name: registryName
  scope: resourceGroup(registryResourceGroup)
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2023-05-01' existing= {
  name: race2appenv 
}

resource containerSecurityProviderApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: managedEnvironments_race2containerappenv_name_resource.id    
    configuration: {    
      activeRevisionsMode: revisionMode  
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
          ]
          probes: useProbes? [
            {
              type: 'Readiness'
               httpGet: {
                 port: 80
                 path: '/api/health/readiness'
                  scheme: 'HTTP'
               }
              periodSeconds: 240
               timeoutSeconds: 5
               initialDelaySeconds: 5
                successThreshold: 1
                failureThreshold: 3
            }
          ] : null
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
