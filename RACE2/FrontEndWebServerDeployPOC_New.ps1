$RESOURCE_GROUP = "POCRACINFRG1402"
$ENVIRONMENT="Production"
$MANAGEDIDENTITY="POCRACINFMI1402"
$APPCONFIG_URL="https://pocracinfac1402.azconfig.io/"
$AZURE_TENANT_ID="6f504113-6b64-43f2-ade9-242e05780007"
$REGISTRY_SERVER="pocracinfcr1402.azurecr.io"
$CONTAINERAPPS_ENVIRONMENT="POCRACINFAE1402"
$CONTAINERAPPNAME ="race2frontendwebserver"
$REMOTE_IMAGENAME="pocracinfcr1402.azurecr.io/race2frontendwebserver:501170"

$identityClientId = (az identity show --resource-group $RESOURCE_GROUP --name $MANAGEDIDENTITY --output json --query "clientId")
$identityResourceId = (az identity show --resource-group $RESOURCE_GROUP --name $MANAGEDIDENTITY --output json --query "id")

 az containerapp create `
  --name $CONTAINERAPPNAME `
  --resource-group $RESOURCE_GROUP `
  --environment $CONTAINERAPPS_ENVIRONMENT `
  --image $REMOTE_IMAGENAME `
  --registry-server $REGISTRY_SERVER `
  --registry-identity $identityResourceId `
  --target-port 8080 `
  --ingress 'external' `
  --cpu 0.5 `
  --memory 1.0Gi `
  --min-replicas 1 `
  --max-replicas 1 `
  --env-vars ASPNETCORE_ENVIRONMENT=$ENVIRONMENT AzureAppConfigURL=$APPCONFIG_URL AZURE_CLIENT_ID=$identityClientId ManagedIdenityClientId=$identityClientId AZURE_TENANT_ID=$AZURE_TENANT_ID `
  --user-assigned $MANAGEDIDENTITY
