using Microsoft.EntityFrameworkCore;
using RedTeamHotChocolate.Ordering.Data;
using RedTeamHotChocolate.Ordering.DataLayer;

var message = """
    ------------------------------------------------------
    RedTeamHotChocolate.Ordering
    ------------------------------------------------------
    """;

Console.WriteLine(message);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<OrderingDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbContext")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin")); // Requires the user to be in the "Admin" role
});

builder.AddGraphQL()
    .AddAuthorization()
    .RegisterDbContextFactory<OrderingDbContext>()
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
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<OrderingDbContext>>();
    using var dbContext = dbContextFactory.CreateDbContext();

    SeedDatabase.Seed(dbContext);
}

else
{
    app.RunWithGraphQLCommands(args);
}