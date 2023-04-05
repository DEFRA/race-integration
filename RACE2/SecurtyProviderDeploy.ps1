$RESOURCE_GROUP = "race2projectrg"
$LOCATION="westeurope"
$ENVIRONMENT="Production"
$MANAGEDIDENTITY="Race2ManagedIdentity"
$CONTAINERAPPS_ENVIRONMENT="race2containerappenv"
$CONTAINERAPPNAME ="race2securityprovider"
$REMOTE_IMAGENAME="race2acr.azurecr.io/race2securityprovider:v1"
$REGISTRY_SERVER="race2acr.azurecr.io"
$REGISTRY_USERNAME="Race2ACR"
$REGISTRY_PASSWORD="3q23FKncYFoNMky5N+4arugBI6FHagtWC07sVgPHdo+ACRB3HwHE"
$APPCONFIG_URL="https://race2appconfig.azconfig.io/"

$identityClientId = (az identity show --resource-group $RESOURCE_GROUP --name $MANAGEDIDENTITY --output json --query "clientId")

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
  --env-vars ASPNETCORE_ENVIRONMENT=$ENVIRONMENT AzureAppConfigURL=$APPCONFIG_URL AZURE_CLIENT_ID=$identityClientId `
  --user-assigned $MANAGEDIDENTITY
  
  