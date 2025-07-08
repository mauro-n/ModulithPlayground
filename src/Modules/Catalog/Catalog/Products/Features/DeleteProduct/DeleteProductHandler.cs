
namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid Id)
        : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductHandler(CatalogDbContext catalogDbContext)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var productToDelete = await catalogDbContext.Products
                .FindAsync([command.Id], cancellationToken);

            if (productToDelete == null)
            {
                throw new Exception($"Product not found: {command.Id}");
            }

            catalogDbContext.Products.Remove(productToDelete);
            await catalogDbContext.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
