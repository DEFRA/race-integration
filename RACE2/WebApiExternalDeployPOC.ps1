$RESOURCE_GROUP = "POCRACINFRG1401"
$ENVIRONMENT="Production"
$MANAGEDIDENTITY="POCRACINFMI1401"
$APPCONFIG_URL="https://pocracinfac1401.azconfig.io/"
$AZURE_TENANT_ID="770a2450-0227-4c62-90c7-4e38537f1102"
$REGISTRY_SERVER="pocracinfcr1401.azurecr.io"
$CONTAINERAPPS_ENVIRONMENT="POCRACINFCE1401"
$CONTAINERAPPNAME ="race2webapiexternal"
$REMOTE_IMAGENAME="pocracinfcr1401.azurecr.io/race2webapiexternal:487418"

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
 