$RESOURCE_GROUP = "DEVRACINFRG1401"
$LOCATION="uksouth"
$LOG_ANALYTICS_WOIRKSPACE="DEVRACINFWS1401"
$CONTAINERAPPS_ENVIRONMENT="DEVRACINFAE1401"

az monitor log-analytics workspace create -n $LOG_ANALYTICS_WOIRKSPACE -g $RESOURCE_GROUP -l $LOCATION

$LOG_ANALYTICS_WORKSPACE_CLIENT_ID=(az monitor log-analytics workspace show --query customerId -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)
$LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET=(az monitor log-analytics workspace get-shared-keys --query primarySharedKey -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv)

az containerapp env create `
   --name $CONTAINERAPPS_ENVIRONMENT `
   --resource-group $RESOURCE_GROUP `
   --logs-workspace-id $LOG_ANALYTICS_WORKSPACE_CLIENT_ID `
   --logs-workspace-key $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET `
   --location "$LOCATION"

