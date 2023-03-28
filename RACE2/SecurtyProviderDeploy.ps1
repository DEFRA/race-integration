$resource_group = "race2projectrg"
$location="westeurope"
$log_analytics_workspace="race2appworkspace"
$CONTAINERAPPS_ENVIRONMENT="race2containerappenv"
$ContainerAppName ="race2securityprovider"
$remoteImageName="race2acr.azurecr.io/race2securityprovider:v1"
$REGISTRY_SERVER="race2acr.azurecr.io"
$REGISTRY_USERNAME="Race2ACR"
$REGISTRY_PASSWORD="3q23FKncYFoNMky5N+4arugBI6FHagtWC07sVgPHdo+ACRB3HwHE"

$LOG_ANALYTICS_WORKSPACE_CLIENT_ID=(az monitor log-analytics workspace show --query customerId -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)
$LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET=(az monitor log-analytics workspace get-shared-keys --query primarySharedKey -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)

az containerapp env create `
   --name $CONTAINERAPPS_ENVIRONMENT `
   --resource-group $RESOURCE_GROUP `
   --logs-workspace-id $LOG_ANALYTICS_WORKSPACE_CLIENT_ID `
   --logs-workspace-key $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET `
   --location "$location"

az containerapp create `
  --name $ContainerAppName `
  --resource-group $RESOURCE_GROUP `
  --environment $CONTAINERAPPS_ENVIRONMENT `
  --image $remoteImageName `
  --registry-server $REGISTRY_SERVER `
  --registry-username $REGISTRY_USERNAME `
  --registry-password $REGISTRY_PASSWORD `
  --target-port 80 `
  --ingress 'external' `
  --cpu 0.5 `
  --memory 1.0Gi `
  --min-replicas 0 `
  --max-replicas 2 `
  --env-vars ASPNETCORE_ENVIRONMENT="Production"
  