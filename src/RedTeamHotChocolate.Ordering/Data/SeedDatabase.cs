using RedTeamHotChocolate.Ordering.DataLayer;
using System.Text.Json;

namespace RedTeamHotChocolate.Ordering.Data;

public static class SeedDatabase
{
    private static readonly string products = "C:\\Development\\hotchocolate-examples\\fusion\\complete\\RedTeamHotChocolate\\src\\RedTeamHotChocolate.Ordering\\products.json";
    public static void Seed(OrderingDbContext context)
    {

        if (File.Exists(products))
        {
            var json = File.ReadAllText(products);

            var allProducts = JsonSerializer.Deserialize<List<ProductEntity>>(json)!;



            if (!context.Orders.Any())
            {
                foreach (var product in allProducts)
                {
                    var orderFaker = new OrderFaker(product.Id, product.Name, product.Description);
                    var orders = orderFaker.Generate(1);
                    context.Orders.AddRange(orders);
                }

                context.SaveChanges();
            }
        }
    }
    public class ProductEntity
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public string Sku { get; init; } = null!;

        public string Description { get; init; } = null!;

        public decimal Price { get; init; }
    }
}

