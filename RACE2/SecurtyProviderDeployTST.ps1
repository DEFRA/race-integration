$RESOURCE_GROUP = "TSTRACINFRG1401"
$ENVIRONMENT="Production"
$MANAGEDIDENTITY="TSTRACINFMI1401"
$APPCONFIG_URL="https://tstracinfac1401.azconfig.io/"
$REGISTRY_SERVER="tstracinfcr1401.azurecr.io"
$CONTAINERAPPS_ENVIRONMENT="TSTRACINFAE1401"
$CONTAINERAPPNAME ="race2securityprovider"
$REMOTE_IMAGENAME="tstracinfcr1401.azurecr.io/race2securityprovider:353889"

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
  
  