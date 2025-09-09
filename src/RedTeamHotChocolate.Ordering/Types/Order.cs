using RedTeamHotChocolate.Ordering.DataLayer;

namespace RedTeamHotChocolate.Ordering.Types;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor.Field("totalLineItems").Description("example of field level auth").Type<DecimalType>()
            .Resolve(descriptor => descriptor.Parent<Order>().Items.Count).Authorize("AdminPolicy");
    }
}