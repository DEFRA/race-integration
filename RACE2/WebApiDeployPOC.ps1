$RESOURCE_GROUP = "race2projectrg"
$ENVIRONMENT="Production"
$MANAGEDIDENTITY="Race2ManagedIdentity"
$REGISTRY_SERVER="race2acr.azurecr.io" 
$APPCONFIG_URL="https://race2appconfig.azconfig.io/"
$CONTAINERAPPS_ENVIRONMENT="race2containerappenv"
$CONTAINERAPPNAME ="race2webapi"
$REMOTE_IMAGENAME="race2acr.azurecr.io/race2webapi:latest"

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
  --min-replicas 1 `
  --max-replicas 2 `
  --env-vars ASPNETCORE_ENVIRONMENT=$ENVIRONMENT AzureAppConfigURL=$APPCONFIG_URL AZURE_CLIENT_ID=$identityClientId `
  --user-assigned $MANAGEDIDENTITY
 