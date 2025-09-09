using RedTeamHotChocolate.Products.DataLayer;

namespace RedTeamHotChocolate.Products.Data;

public static class SeedDatabase
{

    private static string? ComputeFiltePath(string currentDirectory)
    {
        var stack = new Stack<string>();
        stack.Push(currentDirectory);

        while (stack.Any())
        {
            var dir = new DirectoryInfo(stack.Pop());
            var match = dir.GetDirectories().Where(x => x.Name == "RedTeamHotChocolate.Ordering").FirstOrDefault();
            if (match != null)
            {
                return $"{dir.FullName}\\products.json";
            }
            else
            {
                var parent = dir.Parent;
                if (parent != null)
                {
                    stack.Push(parent.FullName);
                }
            }
        }


        return null;
    }
    public static void Seed(ProductsDbContext context)
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
        var path = ComputeFiltePath(".");

        if (!string.IsNullOrWhiteSpace(path))
        {
            var fullPath = new FileInfo(path);

            File.WriteAllText(fullPath.FullName, System.Text.Json.JsonSerializer.Serialize(products));

            Console.WriteLine($"Saved products to {fullPath.FullName}");
        }


    }

}

