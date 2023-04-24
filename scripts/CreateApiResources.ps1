$resourceGroup = "azure-app-service-demo-rg"                                                                                                     
$location = "eastus"                                                                                                                              
$appServicePlanName = "azure-app-service-demo-sp"                                                                                              
$appServiceName = "marvellous-api"                                                                                                              
az group create --name $resourceGroup --location $location                                                                                        
az appservice plan create --name $appServicePlanName  --resource-group $resourceGroup --sku free                                               
az webapp create --name $appServiceName --resource-group $resourceGroup --plan $appServicePlanName                                        