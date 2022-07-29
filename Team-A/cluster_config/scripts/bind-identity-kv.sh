# Set the User Assigned ID access to the Key Vault
kv=rm-dev
nrg=eu1-dev-aks-1-nrg
mi=eu1-dev-aks-1-agentpool
secret1=B64PAT
namespace=weather-eu-namespace

principalId=$(az identity show -g $nrg --name $mi --query principalId -o tsv)
az keyvault set-policy --name $kv --object-id $principalId --secret-permissions get

clientid=$(az identity show -g $nrg --name $mi --query clientId -o tsv)
tenantId=$(az account show --query tenantId -o tsv)