$RESOURCE_GROUP = "race2projectrg"
$LOCATION="westeurope"
$CONTAINERAPPS_ENVIRONMENT="race2containerappenv"
$CONTAINERAPPNAME ="race2securityprovider1"
$REMOTE_IMAGENAME="race2acr.azurecr.io/race2securityprovider:v2"
$REGISTRY_SERVER="race2acr.azurecr.io"
$REGISTRY_USERNAME="Race2ACR"
$REGISTRY_PASSWORD="3q23FKncYFoNMky5N+4arugBI6FHagtWC07sVgPHdo+ACRB3HwHE"

az containerapp create `
  --name $CONTAINERAPPNAME `
  --resource-group $RESOURCE_GROUP `
  --environment $CONTAINERAPPS_ENVIRONMENT `
  --image $REMOTE_IMAGENAME `
  --registry-server $REGISTRY_SERVER `
  --registry-username $REGISTRY_USERNAME `
  --registry-password $REGISTRY_PASSWORD `
  --target-port 80 `
  --ingress 'external' `
  --cpu 0.5 `
  --memory 1.0Gi `
  --min-replicas 0 `
  --max-replicas 2 `
  --env-vars ASPNETCORE_ENVIRONMENT="Production" AzureAppConfigURL="https://race2appconfig.azconfig.io/" AZURE_APPCONFIGURATION_CONNECTIONSTRING="Endpoint=https://race2appconfig.azconfig.io;Id=Qb5j-l9-s0:NgoTP/YDtpUUr9NU4JUs;Secret=6XI19b3l46iqeePJ8xlAYYeevyp0NSL/mUvz8rNM7/M=" `
  --user-assigned "Race2ManagedIdentity"
  
  