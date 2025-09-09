using GreenDonut.Data;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using RedTeamHotChocolate.Ordering.DataLayer;

namespace RedTeamHotChocolate.Ordering.Types;

[QueryType]
public static class Query
{
    [UseFiltering]
    public static async Task<List<Order>> GetOrders(OrderingDbContext orderingDbContext,
              IResolverContext context, QueryContext<Order>? queryContext = null)
    {
        var queryable = orderingDbContext.Orders.AsNoTracking();
        if (context.IsSelected("items"))
        {
            queryContext = queryContext?.Include(x => x.Items);
        }

        return await queryable
            .With(queryContext)
            .ToListAsync();

    }
}