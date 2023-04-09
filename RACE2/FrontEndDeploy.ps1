$RESOURCE_GROUP = "race2projectrg"
$LOCATION="westeurope"
$APPCONFIG_URL="https://race2appconfig.azconfig.io/"
$CONTAINERAPPS_ENVIRONMENT="race2containerappenv"
$CONTAINERAPPNAME ="race2frontend1"
$REMOTE_IMAGENAME="race2acr.azurecr.io/race2frontend:v2"
$REGISTRY_SERVER="race2acr.azurecr.io"
$REGISTRY_USERNAME="Race2ACR"
$REGISTRY_PASSWORD="3q23FKncYFoNMky5N+4arugBI6FHagtWC07sVgPHdo+ACRB3HwHE"  
$MANAGEDIDENTITY="Race2ManagedIdentity"
$identityClientId = (az identity show --resource-group $RESOURCE_GROUP --name $MANAGEDIDENTITY --output json --query "clientId")
$identityResourceId = (az identity show --resource-group $RESOURCE_GROUP --name $MANAGEDIDENTITY --output json --query "id")

 az containerapp create `
  --name $CONTAINERAPPNAME `
  --resource-group $RESOURCE_GROUP `
  --environment $CONTAINERAPPS_ENVIRONMENT `
  --image $REMOTE_IMAGENAME `
  --registry-server $REGISTRY_SERVER `
  --registry-identity $identityResourceId `
  --target-port 80 `
  --ingress 'external' `
  --cpu 0.5 `
  --memory 1.0Gi `
  --min-replicas 0 `
  --max-replicas 2 `
  --env-vars ASPNETCORE_ENVIRONMENT="Production" AzureAppConfigURL=$APPCONFIG_URL AZURE_CLIENT_ID=$identityClientId `
  --user-assigned $MANAGEDIDENTITY