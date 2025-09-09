namespace RedTeamHotChocolate.Ordering.DataLayer;

public class Order
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<LineItem> Items { get; set; } = null!;
}


