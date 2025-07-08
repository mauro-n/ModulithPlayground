namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category)
        : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);

    internal class GetProductByCategoryHandler(CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle
            (
            GetProductByCategoryQuery query,
            CancellationToken cancellationToken
            )
        {
            var products = await catalogDbContext.Products
                .AsNoTracking().Where(el => el.Category.Contains(query.Category))
                .OrderBy(el => el.Name)
                .ToListAsync();

            var productDtos = products.Adapt<List<ProductDto>>();

            return new GetProductByCategoryResult(productDtos);
        }
    }
}
