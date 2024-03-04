param azureTenanatId string
param location string
param race2appenv string
param registryName string
param resourcegroup string
param useExternalIngress bool = false
param containerPort int
param managedidentity string
param appConfigURL string
param aspnetCoreEnv string 
param containerAppName string
param containerImage string
param minReplicas int
param maxReplicas int
param tag string
var tagVal=json(tag)
var subscriptionid = subscription().subscriptionId
param transport string
param allowInsecure bool

resource managedEnvironments_race2containerappenv_name_resource 'Microsoft.App/managedEnvironments@2023-08-01-preview' existing= {
  name: race2appenv 
}

resource managedIdentity_resource 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' existing= {
  name: managedidentity 
}

resource containerWebApiExternalApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: managedEnvironments_race2containerappenv_name_resource.id    
    configuration: { 
      registries: [
        {
          server: '${registryName}.azurecr.io'
          identity: '/subscriptions/${subscriptionid}/resourcegroups/${resourcegroup}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/${managedidentity}'
        }
      ]
      ingress: {
        external: useExternalIngress
        targetPort: containerPort
        transport: transport
        allowInsecure: allowInsecure
      }
    }
    template: {
      containers: [
        {
          env: [
            {
              name: 'AZURE_TENANT_ID'
              value: azureTenanatId
            }
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
            {
              name: 'ManagedIdenityClientId'
              value: managedIdentity_resource.properties.clientId
            }
          ]          
          image: '${containerImage}:${tagVal.tag}' //concat('${webapicontainerImage}',':','${tagVal.tag}')
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
