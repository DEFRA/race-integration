param containerAppName string
param subscriptionid string 
param location string
param race2appenv string
param registryName string
param registryResourceGroup string
param resourcegroup string
param useExternalIngress bool = false
param containerPort int
param containerImage string
param managedidentity string
param revisionMode string
param useProbes bool
param minReplicas int
param maxReplicas int
param appConfigURL string
param aspnetCoreEnv string 
param azureClientId string
param tag string
var tagVal = json(tag)

resource registry 'Microsoft.ContainerRegistry/registries@2022-12-01' existing = {
  name: registryName
  scope: resourceGroup(registryResourceGroup)
}

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2023-05-01' existing= {
  name: race2appenv 
}

resource containerWebApiApp 'Microsoft.App/containerApps@2023-05-01' = {
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
        transport: 'auto'
        traffic: [
          {
            latestRevision: true
            weight: 100
          }
        ]
      }
    }
    template: {
      containers: [
        {
          image: '${containerImage}:${tagVal.tag}' //concat('${ontainerImage}',':','${tagVal.tag}')
          name: containerAppName
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
