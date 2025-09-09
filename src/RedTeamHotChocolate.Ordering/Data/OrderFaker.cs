using Bogus;
using RedTeamHotChocolate.Ordering.DataLayer;


namespace RedTeamHotChocolate.Ordering.Data;

public class OrderFaker : Faker<Order>
{
    private static int _productId;

    public int getProductId()
    {
        Interlocked.Increment(ref _productId);
        return _productId;
    }
    public OrderFaker(int id, string name, string description)
    {
        RuleFor(o => o.Name, f => f.Commerce.ProductName());
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.Items, f => new List<LineItem>
        {
            new LineItem
            {
                ProductId = id,
                Quantity = f.Random.Int(1, 10)
            }
        });
    }
}
