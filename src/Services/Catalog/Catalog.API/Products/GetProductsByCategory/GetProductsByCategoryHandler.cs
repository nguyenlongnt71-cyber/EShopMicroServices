
using System.Collections;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCategoryResult.Handler called with {@query}", query);
            var products = await session.Query<Product>().Where(c => c.Category.Contains(query.category)).ToListAsync();
            if (!products.Any())
            {
                throw new ProductNotFoundException();
            }
            return new GetProductsByCategoryResult(products);
        }
    }
}
