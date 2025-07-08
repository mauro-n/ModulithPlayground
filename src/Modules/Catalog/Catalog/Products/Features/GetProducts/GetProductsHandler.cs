namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery()
        : IQuery<GetProductResult>;

    public record GetProductResult(IEnumerable<ProductDto> Products);

    internal class GetProductsHandler(CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await catalogDbContext.Products
                .AsNoTracking()
                .OrderBy(el => el.Name)
                .ToListAsync(cancellationToken);

            var productDtos = products.Adapt<List<ProductDto>>();
            return new GetProductResult(productDtos);
        }
    }
}
