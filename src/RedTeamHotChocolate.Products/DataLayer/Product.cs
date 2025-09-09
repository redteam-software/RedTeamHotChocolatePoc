namespace RedTeamHotChocolate.Products.DataLayer;

public record Product
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Sku { get; init; } = null!;

    public string Description { get; init; } = null!;

    public decimal Price { get; init; }
}