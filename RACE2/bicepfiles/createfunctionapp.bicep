param location string
param functionappName string

param serverfarms_ASP_PRDRACINFRG1401_85f7_externalid string = '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/PRDRACINFRG1401/providers/Microsoft.Web/serverfarms/PRDRACINFAF1401'
param virtualNetworks_PRDRACINFVN1401_externalid string = '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/PRDRACINFRG1401/providers/Microsoft.Network/virtualNetworks/PRDRACINFVN1401'

resource functionappresource 'Microsoft.Web/sites@2023-01-01' = {
  name: functionappName
  location: location
  tags: {
    'hidden-link: /app-insights-resource-id': '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/PRDRACINFRG1401/providers/microsoft.insights/components/PRDRACINFAI1401'
    ServiceCode: 'RAC'
    'hidden-link: /app-insights-instrumentation-key': 'aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76'
    'hidden-link: /app-insights-conn-string': 'InstrumentationKey=aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76;IngestionEndpoint=https://uksouth-1.in.applicationinsights.azure.com/;LiveEndpoint=https://uksouth.livediagnostics.monitor.azure.com/'
  }
  kind: 'functionapp,linux'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: 'pocracinffa1401.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: 'pocracinffa1401.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serverfarms_ASP_PRDRACINFRG1401_85f7_externalid
    reserved: true
    isXenon: false
    hyperV: false
    vnetRouteAllEnabled: true
    vnetImagePullEnabled: false
    vnetContentShareEnabled: false
    siteConfig: {
      numberOfWorkers: 1
      linuxFxVersion: 'DOTNET|6.0'
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 1
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: false
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '8EAA32A1C9B818D1D859E3B66BDD96C167E43FA32EB83468ADB6BE2D6B0F1B25'
    containerSize: 1536
    dailyMemoryTimeQuota: 0
    httpsOnly: true
    redundancyMode: 'None'
    publicNetworkAccess: 'Enabled'
    storageAccountRequired: false
    virtualNetworkSubnetId: '${virtualNetworks_PRDRACINFVN1401_externalid}/subnets/subnetazfn'
    keyVaultReferenceIdentity: 'SystemAssigned'
  }
}

resource sites_PRDRACINFFA1401_name_ftp 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2023-01-01' = {
  parent: functionappresource
  name: 'ftp'
  location: location
  tags: {
    'hidden-link: /app-insights-resource-id': '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/PRDRACINFRG1401/providers/microsoft.insights/components/PRDRACINFAI1401'
    ServiceCode: 'RAC'
    'hidden-link: /app-insights-instrumentation-key': 'aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76'
    'hidden-link: /app-insights-conn-string': 'InstrumentationKey=aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76;IngestionEndpoint=https://uksouth-1.in.applicationinsights.azure.com/;LiveEndpoint=https://uksouth.livediagnostics.monitor.azure.com/'
  }
  properties: {
    allow: true
  }
}

resource sites_PRDRACINFFA1401_name_scm 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2023-01-01' = {
  parent: functionappresource
  name: 'scm'
  location: location
  tags: {
    'hidden-link: /app-insights-resource-id': '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/PRDRACINFRG1401/providers/microsoft.insights/components/PRDRACINFAI1401'
    ServiceCode: 'RAC'
    'hidden-link: /app-insights-instrumentation-key': 'aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76'
    'hidden-link: /app-insights-conn-string': 'InstrumentationKey=aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76;IngestionEndpoint=https://uksouth-1.in.applicationinsights.azure.com/;LiveEndpoint=https://uksouth.livediagnostics.monitor.azure.com/'
  }
  properties: {
    allow: true
  }
}

resource sites_PRDRACINFFA1401_name_web 'Microsoft.Web/sites/config@2023-01-01' = {
  parent: functionappresource
  name: 'web'
  location: location
  tags: {
    'hidden-link: /app-insights-resource-id': '/subscriptions/d9cce027-07b6-4275-a215-dd8d52b9d469/resourceGroups/PRDRACINFRG1401/providers/microsoft.insights/components/PRDRACINFAI1401'
    ServiceCode: 'RAC'
    'hidden-link: /app-insights-instrumentation-key': 'aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76'
    'hidden-link: /app-insights-conn-string': 'InstrumentationKey=aeeee9e0-13d7-4554-ac5c-7d9f69a0ea76;IngestionEndpoint=https://uksouth-1.in.applicationinsights.azure.com/;LiveEndpoint=https://uksouth.livediagnostics.monitor.azure.com/'
  }
  properties: {
    numberOfWorkers: 1
    defaultDocuments: [
      'Default.htm'
      'Default.html'
      'Default.asp'
      'index.htm'
      'index.html'
      'iisstart.htm'
      'default.aspx'
      'index.php'
    ]
    netFrameworkVersion: 'v4.0'
    linuxFxVersion: 'DOTNET|6.0'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: '$PRDRACINFFA1401'
    scmType: 'None'
    use32BitWorkerProcess: false
    webSocketsEnabled: false
    alwaysOn: false
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: false
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetName: '81f3dbb5-a160-4b3a-97d2-e0e7b08ddae1_subnetazfn'
    vnetRouteAllEnabled: true
    vnetPrivatePortsCount: 0
    publicNetworkAccess: 'Enabled'
    cors: {
      allowedOrigins: [
        'https://portal.azure.com'
      ]
      supportCredentials: false
    }
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.2'
    ftpsState: 'FtpsOnly'
    preWarmedInstanceCount: 1
    functionAppScaleLimit: 0
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 1
    azureStorageAccounts: {}
  }
}

resource sites_PRDRACINFFA1401_name_111502b0_7e77_49b5_b001_9f7482c6288e 'Microsoft.Web/sites/deployments@2023-01-01' = {
  parent: functionappresource
  name: '111502b0-7e77-49b5-b001-9f7482c6288e'
  location: location
  properties: {
    status: 4
    author_email: 'N/A'
    author: 'N/A'
    deployer: 'Push-Deployer'
    message: 'Created via a push deployment'
    start_time: '2023-12-11T11:19:53.6728057Z'
    end_time: '2023-12-11T11:19:57.1727136Z'
    active: true
  }
}

resource sites_PRDRACINFFA1401_name_4bea7815_6f7d_4341_b4e5_c3cd3298ea77 'Microsoft.Web/sites/deployments@2023-01-01' = {
  parent: functionappresource
  name: '4bea7815-6f7d-4341-b4e5-c3cd3298ea77'
  location: location
  properties: {
    status: 4
    author_email: 'N/A'
    author: 'N/A'
    deployer: 'Push-Deployer'
    message: 'Created via a push deployment'
    start_time: '2023-12-11T11:15:16.9695682Z'
    end_time: '2023-12-11T11:15:19.6396852Z'
    active: false
  }
}

resource sites_PRDRACINFFA1401_name_63b61217_6ad8_4816_a830_6f5c5dd6ede4 'Microsoft.Web/sites/deployments@2023-01-01' = {
  parent: functionappresource
  name: '63b61217-6ad8-4816-a830-6f5c5dd6ede4'
  location: location
  properties: {
    status: 4
    author_email: 'N/A'
    author: 'N/A'
    deployer: 'Push-Deployer'
    message: 'Created via a push deployment'
    start_time: '2023-12-11T11:12:48.2860219Z'
    end_time: '2023-12-11T11:12:51.4329311Z'
    active: false
  }
}

resource sites_PRDRACINFFA1401_name_MoveMaliciousBlobEventTrigger 'Microsoft.Web/sites/functions@2023-01-01' = {
  parent: functionappresource
  name: 'MoveMaliciousBlobEventTrigger'
  location: location
  properties: {
    script_root_path_href: 'https://pocracinffa1401.azurewebsites.net/admin/vfs/home/site/wwwroot/MoveMaliciousBlobEventTrigger/'
    script_href: 'https://pocracinffa1401.azurewebsites.net/admin/vfs/home/site/wwwroot/bin/RACE2.DefenderScanAzurefn.dll'
    config_href: 'https://pocracinffa1401.azurewebsites.net/admin/vfs/home/site/wwwroot/MoveMaliciousBlobEventTrigger/function.json'
    test_data_href: 'https://pocracinffa1401.azurewebsites.net/admin/vfs/home/data/Functions/sampledata/MoveMaliciousBlobEventTrigger.dat'
    href: 'https://pocracinffa1401.azurewebsites.net/admin/functions/MoveMaliciousBlobEventTrigger'
    config: {}
    language: 'DotNetAssembly'
    isDisabled: false
  }
}

resource sites_PRDRACINFFA1401_name_sites_PRDRACINFFA1401_name_azurewebsites_net 'Microsoft.Web/sites/hostNameBindings@2023-01-01' = {
  parent: functionappresource
  name: '${functionappName}.azurewebsites.net'
  location: location
  properties: {
    siteName: 'PRDRACINFFA1401'
    hostNameType: 'Verified'
  }
}

resource sites_PRDRACINFFA1401_name_81f3dbb5_a160_4b3a_97d2_e0e7b08ddae1_subnetazfn 'Microsoft.Web/sites/virtualNetworkConnections@2023-01-01' = {
  parent: functionappresource
  name: '81f3dbb5-a160-4b3a-97d2-e0e7b08ddae1_subnetazfn'
  location: location
  properties: {
    vnetResourceId: '${virtualNetworks_PRDRACINFVN1401_externalid}/subnets/subnetazfn'
    isSwift: true
  }
}
