using RedTeamHotChocolate.Ordering.DataLayer;

namespace RedTeamHotChocolate.Ordering.Types;


[ObjectType<LineItem>]
public static partial class LineItemType
{
    static partial void Configure(IObjectTypeDescriptor<LineItem> descriptor)
    {
        descriptor.Ignore(x => x.ProductId);
    }

    public static Product GetProduct([Parent] LineItem lineItem)
        => new(lineItem.ProductId);
}

public sealed record Product(int Id);