
Write-Host "Initializing Databases"
push-location .\src\RedTeamHotChocolate.Ordering
Write-Host "Creating Database for Ordering service..."
dotnet ef migrations add InitialCreate
dotnet ef database update
pop-location

push-location .\src\RedTeamHotChocolate.Products
Write-Host "Creating Database for Products service..."
dotnet ef migrations add InitialCreate
dotnet ef database update
pop-location


push-location .\src\RedTeamHotChocolate.Products
Write-Host "Seeding Database for Products service..."
dotnet run -- seed
pop-location


push-location .\src\RedTeamHotChocolate.Ordering
Write-Host "Seeding Database for Ordering service..."
dotnet run -- seed
pop-location


