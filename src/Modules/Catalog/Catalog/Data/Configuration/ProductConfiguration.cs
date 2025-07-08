namespace Catalog.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(el => el.Id);
            builder.Property(el => el.Name).HasMaxLength(50).IsRequired();
            builder.Property(el => el.Category).IsRequired();
            builder.Property(el => el.Description).HasMaxLength(200);
            builder.Property(el => el.ImageFile).HasMaxLength(100);
            builder.Property(el => el.Price).IsRequired();
        }
    }
}
