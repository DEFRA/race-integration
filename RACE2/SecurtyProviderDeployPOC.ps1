$RESOURCE_GROUP = "POCRACINFRG1402"
$TENANT_ID = '6f504113-6b64-43f2-ade9-242e05780007'
$LOCATION="uksouth"
$CONTAINERAPPS_ENVIRONMENT="POCRACINFAE1402"
$CONTAINERAPPNAME ="race2securityprovider"
$REMOTE_IMAGENAME="pocracinfcr1402.azurecr.io/race2securityprovider:475333"
$MANAGEDIDENTITY="POCRACINFMI1402"
$REGISTRY_SERVER="pocracinfcr1402.azurecr.io"
$APPCONFIG_URL="https://POCRACINFAC1402.azconfig.io/"
$ENVIRONMENT="Production"

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
  --env-vars AZURE_TENANT_ID=$TENANT_ID ASPNETCORE_ENVIRONMENT=$ENVIRONMENT AzureAppConfigURL=$APPCONFIG_URL AZURE_CLIENT_ID=$identityClientId ManagedIdenityClientId=$identityClientId `
  --user-assigned $MANAGEDIDENTITY
