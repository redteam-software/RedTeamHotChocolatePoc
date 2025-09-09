using RedTeamHotChocolate.Products.DataLayer;
using RedTeamHotChocolate.Products.Types;

namespace RedTeamHotChocolate.Products.Data;

public static class SeedDatabase
{
    public static void Seed(ProductsDbContext context)
    {
        if (!context.Products.Any())
        {
            var orderFaker = new ProductFaker();
            var orders = orderFaker.Generate(100);
            context.Products.AddRange(orders);
            context.SaveChanges();


            //get all data and write to file

            var products = new List<Product>();
            foreach (var product in context.Products)
            {
                products.Add(product);
            }

            File.WriteAllText("C:\\Development\\hotchocolate-examples\\fusion\\complete\\RedTeamHotChocolate\\src\\RedTeamHotChocolate.Ordering\\products.json", System.Text.Json.JsonSerializer.Serialize(products));
        }
    }

}

