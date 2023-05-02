param subscriptionid string 
param tenantId string =  subscription().tenantId
param containerregistryName string 
param race2appenvName string
param namespaces_ServiceBus_name string
param storageAccountName string
param resourcegroup string
param managedidentity string
param logAnalyticsWorkspaceName string
param appconfigName string
param keyvaultName string
param location string = resourceGroup().location
@secure()
param servers_race2sqlserver_name string
param administratorLogin string
@secure()
param administratorLoginPassword string
param servers_race2sqldb_name string
param frontEndContainerAppName string
param securityProviderContainerAppName string
param webApiContainerAppName string
param registryName string
param registryResourceGroup string
param useExternalIngress bool = false
param containerPort int
param frontendcontainerImage string
param securityprovidercontainerImage string
param webapicontainerImage string

module createmanagedidentitymodule 'createmanagedidentity.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'managedidentitydeploy'
  params: {
    location: location
    miname: managedidentity
  }
}

module createcontainerregistrymodule 'createcontainerregistry.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'containerregistrydeploy'
  params: {
    location: location
    subscriptionid: subscriptionid
    resourcegroup: resourcegroup
    containerregistryname: containerregistryName
    managedidentity: managedidentity
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createservicebusmodule 'createservicebus.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'servicebusdeploy'
  params: {
    location: location
    namespaces_ServiceBus_name: namespaces_ServiceBus_name
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createstorageaccountmodule 'createstorageaccount.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'stoareacoountdeploy'
  params: {
    location: location
    storageAccountname: storageAccountName
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createkeyvaultmodule 'createkeyvault.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'keyvaultdeploy'
  params: {
    location: location
    keyvaultName: keyvaultName
    tenantId: tenantId
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createappconfigmodule 'createappconfig.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'appconfigdeploy'
  params: {
    location: location
    subscriptionid: subscriptionid
    resourcegroup: resourcegroup
    appconfigName: appconfigName
    managedidentity: managedidentity
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createsqlservermodule 'createsqlserver.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'sqlserverdeploy'
  params: {
    location: location
    servers_race2sqlserver_name: servers_race2sqlserver_name
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    servers_race2sqldb_name: servers_race2sqldb_name
  }
  dependsOn: [
    createmanagedidentitymodule
  ]
}

module createappworkspacemodule 'createappworkspace.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'appworkspacedeploy'
  params: {
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
  }
}

module createcontainerappenvmodule 'createcontainerappenv.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'race2appenvdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    lawsCustromerId: createappworkspacemodule.outputs.customerId
    lawsSharedKey: createappworkspacemodule.outputs.sharedKey
  }
  dependsOn: [
    createappworkspacemodule
  ]
}

module createfrontendcontainerappmodule 'createfrontendcontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'frontendcontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    frontEndContainerAppName: frontEndContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    frontendcontainerImage: frontendcontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
  }
  dependsOn: [
    createcontainerappenvmodule
  ]
}

module createsecurityprovidercontainerappmodule 'createsecurityprovidercontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'securityprovidercontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    securityProviderContainerAppName: securityProviderContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    securityprovidercontainerImage: securityprovidercontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
  }
  dependsOn: [
    createcontainerappenvmodule
  ]
}
module createwebapicontainerappmodule 'createwebapicontainerapp.bicep' = {
  scope: resourceGroup(resourcegroup)
  name: 'webapicontainerappdeploy'
  params: {
    location: location
    race2appenv: race2appenvName
    webApiContainerAppName: webApiContainerAppName
    registryName: registryName
    registryResourceGroup: registryResourceGroup
    resourcegroup: resourcegroup
    useExternalIngress: useExternalIngress
    containerPort: containerPort
    webapicontainerImage: webapicontainerImage
    managedidentity: managedidentity
    subscriptionid: subscriptionid
  }
  dependsOn: [
    createcontainerappenvmodule
  ]
}

