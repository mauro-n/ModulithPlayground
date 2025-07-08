namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler(CatalogDbContext dbContext)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var currentProduct = await dbContext.Products.FindAsync([command.Product.Id], cancellationToken);

            if (currentProduct == null)
            {
                throw new Exception($"Product not found: {command.Product.Id}");
            }

            UpdateProductWithNewValues(currentProduct, command.Product);
            dbContext.Products.Update(currentProduct);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product currentProduct, ProductDto product)
        {
            currentProduct.Update(
                product.Name,
                product.Category,
                product.Description,
                product.ImageFile,
                product.Price
                );
        }
    }
}
