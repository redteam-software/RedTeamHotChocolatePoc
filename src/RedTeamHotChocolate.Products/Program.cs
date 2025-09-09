using Microsoft.EntityFrameworkCore;
using RedTeamHotChocolate.Products.Data;
using RedTeamHotChocolate.Products.DataLayer;

var message = """
    ------------------------------------------------------
    RedTeamHotChocolate.Products
    ------------------------------------------------------
    """;

Console.WriteLine(message);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<ProductsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbContext")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin")); // Requires the user to be in the "Admin" role
});

builder.AddGraphQL()
    .AddAuthorization()
    .RegisterDbContextFactory<ProductsDbContext>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddPagingArguments()
    .AddQueryContext()
    .AddTypes();

var app = builder.Build();

app.MapGraphQL();

if (args.Contains("seed"))
{
    using var scope = app.Services.CreateScope();
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ProductsDbContext>>();
    using var dbContext = dbContextFactory.CreateDbContext();

    SeedDatabase.Seed(dbContext);
}
else
{
    app.RunWithGraphQLCommands(args);
}