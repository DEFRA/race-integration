$RESOURCE_GROUP = "race2projectrg"
$RESOURCE_GROUP = "DEVRACINFRG1401"
$ENVIRONMENT="Production"
$MANAGEDIDENTITY="DEVRACINFMI1401"
$APPCONFIG_URL="https://devracinfac1401.azconfig.io/"
$REGISTRY_SERVER="devracinfcr1401.azurecr.io"
$CONTAINERAPPS_ENVIRONMENT="DEVRACINFAE1401"
$CONTAINERAPPNAME ="race2webapi"
$REMOTE_IMAGENAME="devracinfcr1401.azurecr.io/race2webapi:latest"

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
 