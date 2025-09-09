namespace RedTeamHotChocolate.Ordering.DataLayer;

public class LineItem
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int ProductId { get; set; }
}



public sealed record Product(int Id);