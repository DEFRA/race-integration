$RESOURCE_GROUP = "race2projectrg"
$LOCATION="westeurope"
$CONTAINERAPPS_ENVIRONMENT="race2containerappenv"
$CONTAINERAPPNAME ="race2frontend"
$REMOTE_IMAGENAME="race2acr.azurecr.io/race2frontend:v1"
$REGISTRY_SERVER="race2acr.azurecr.io"
$REGISTRY_USERNAME="Race2ACR"
$REGISTRY_PASSWORD="3q23FKncYFoNMky5N+4arugBI6FHagtWC07sVgPHdo+ACRB3HwHE"  

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
  --env-vars ASPNETCORE_ENVIRONMENT="Production"