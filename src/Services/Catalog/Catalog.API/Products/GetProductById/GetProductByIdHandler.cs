
namespace Catalog.API.Products.GetProductById
{
    public record GetProductQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            //logger.LogInformation("GetProductsQueryHandler.Handler called with {@id}", query);
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
