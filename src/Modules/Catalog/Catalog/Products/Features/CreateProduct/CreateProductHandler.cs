namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto Product)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(el => el.Product.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(el => el.Product.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(el => el.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(el => el.Product.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }

    internal class CreateProductCommandHandler(
        CatalogDbContext dbContext,
        IValidator<CreateProductCommand> validator
        )
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(
            CreateProductCommand command,
            CancellationToken cancellationToken
            )
        {
            var product = CreateNewProduct(command.Product);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(ProductDto dto)
        {
            var product = Product.Create(
                Guid.NewGuid(),
                dto.Name,
                dto.Category,
                dto.Description,
                dto.ImageFile,
                dto.Price
                );

            return product;
        }
    }
}
