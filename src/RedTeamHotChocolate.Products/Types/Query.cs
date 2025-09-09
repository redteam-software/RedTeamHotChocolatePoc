using GreenDonut.Data;
using HotChocolate.Fusion.SourceSchema.Types;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using RedTeamHotChocolate.Products.DataLayer;

namespace RedTeamHotChocolate.Products.Types;

[QueryType]
public static class Query
{
    [UseFiltering]
    public static async Task<List<Product>> GetProducts(ProductsDbContext productsDbContext,
        IResolverContext context, QueryContext<Product>? queryContext = null)
    {
        return await productsDbContext
            .Products
            .AsNoTracking()
            .With(queryContext)
            .AsQueryable().ToListAsync();
    }

    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static async Task<Connection<Product>> SearchProducts(
        ProductsDbContext productsDbContext,
        PagingArguments pagingArguments,
       IResolverContext context, QueryContext<Product>? queryContext = null)
    {
        return await productsDbContext
            .Products
            .OrderBy(x => x.Id)
            .AsNoTracking()
            .With(queryContext)
            .AsQueryable().ToPageAsync(pagingArguments).ToConnectionAsync();
    }
}

/// <summary>
/// See https://chillicream.com/docs/fusion/v16/lookups/
/// </summary>
public static class ProductOperations
{
    /// <summary>
    /// This is  an efficient lookup method employing batching. It will be invoked by Fusion when attempting to lookup related entities.  HC uses naming conventions
    /// to identity potential lookup candidates.  Because the related schema has a field named "id", the input parameter to our method must match; otherwise, an execution 
    /// exception will occur.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="productsDbContext"></param>
    /// <returns></returns>
    [Query]
    [Lookup]
    [Internal]
    public static async Task<List<Product>> GetProductsById(List<int> id, ProductsDbContext context)
    {
        return await context.Products
             .Where(t => id.Contains(t.Id))
             .ToListAsync();
    }

    /// <summary>
    /// This is  an efficient lookup method using a data loader.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="productsDbContext"></param>
    /// <returns></returns>
    [Query]
    [Lookup]
    [Internal]
    public static async Task<Product?> GetProductById(int id, ProductsDbContext context)
    {
        return await context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
    }


}


