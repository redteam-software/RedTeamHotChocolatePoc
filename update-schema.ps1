Write-Host "Updating GraphQL schemas for RedTeamHotChocolate services..."
push-location .\src\RedTeamHotChocolate.Ordering
Write-Host "Updating schema for Ordering service..."
dotnet run -- schema export --output schema.graphql
fusion subgraph pack
pop-location

push-location .\src\RedTeamHotChocolate.Products
Write-Host "Updating schema for Products service..."
dotnet run -- schema export --output schema.graphql
fusion subgraph pack
pop-location

push-location .\src\RedTeamHotChocolate.Gateway
Write-Host "Updating schema for Gateway service..." 
fusion compose -p gateway.fgp -s ../RedTeamHotChocolate.Ordering
fusion compose -p gateway.fgp -s ../RedTeamHotChocolate.Products
pop-location
