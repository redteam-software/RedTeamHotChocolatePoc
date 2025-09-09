
using RedTeamHotChocolate.Ordering.DataLayer;
using Order = RedTeamHotChocolate.Ordering.DataLayer.Order;
namespace RedTeamHotChocolate.Ordering.Types
{
    public record LinItemInput(int Id, int Quantity, int ProductId);
    public record OrderInput(int Id, string Name, string Description, List<LinItemInput> LinItems);

    public record OrderCreatedPayload(Order Order);

    [MutationType]
    public static class Mutation
    {
        public static async Task<OrderCreatedPayload> CreateOrder(OrderInput orderInput, OrderingDbContext orderingDbContext)
        {
            var order = new Order
            {
                Name = orderInput.Name,
                Description = orderInput.Description,
                Items = orderInput.LinItems.Select(li => new LineItem
                {
                    ProductId = li.ProductId,
                    Quantity = li.Quantity
                }).ToList()
            };
            orderingDbContext.Orders.Add(order);
            await orderingDbContext.SaveChangesAsync();
            return new(order);
        }
    }
}
