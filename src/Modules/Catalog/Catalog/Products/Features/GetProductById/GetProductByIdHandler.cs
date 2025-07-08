namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(ProductDto Product);

    internal class GetProductByIdHandler(CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await catalogDbContext
                .Products
                .AsNoTracking()
                .FirstOrDefaultAsync(el => el.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new Exception($"Product not found: {request.Id}");
            }

            var productDto = product.Adapt<ProductDto>();
            return new GetProductByIdResult(productDto);
        }
    }
}
