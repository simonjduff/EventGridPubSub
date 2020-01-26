param (
    [Parameter(Mandatory=$true)][string]$resourceGroup, 
    [Parameter(Mandatory=$true)][string]$region
)

function LoggedOut{
    $loginState = az account show
    return $loginState -eq $null
}

if (LoggedOut){
    az login
}

az configure --defaults group=$resourceGroup location=$region

Write-Host "Resource group"
az group create -l $region -n $resourceGroup

Write-Host "Storage"
$storage = az storage account create --name "$($resourceGroup.ToLower())storage" --sku Standard_LRS | ConvertFrom-Json

Write-Host "Function app"
$functionApp = az functionapp create --name "$($resourceGroup.ToLower())functions" --storage-account  $storage.name --runtime node --consumption-plan-location $region | ConvertFrom-Json

Write-Host "Function"
cd functions
func azure functionapp publish $functionApp.name
cd ..

Write-Host "Topic"
$topic = az eventgrid topic create --name "$($resourceGroup.ToLower())topic" | ConvertFrom-Json

Write-Host "Topic subscription"
$keys = az rest --method post --uri "https://management.azure.com$($functionApp.id)/functions/EventGridTableWriter/listKeys?api-version=2018-02-01" | ConvertFrom-Json
az eventgrid event-subscription create --endpoint "https://$($functionApp.name).azurewebsites.net/runtime/webhooks/eventgrid?functionName=EventGridTableWriter&code={$($keys.default)}" `
    --endpoint-type webhook --name EventGridTableWriterSubscription