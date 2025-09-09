using Bogus;
using RedTeamHotChocolate.Products.DataLayer;

namespace RedTeamHotChocolate.Products.Data;

public class ProductFaker : Faker<Product>
{

    public ProductFaker()
    {
        RuleFor(o => o.Name, f => f.Commerce.ProductName());
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.Sku, f => f.Commerce.Ean13());
        RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price(5, 1000, 2)));
    }


}
