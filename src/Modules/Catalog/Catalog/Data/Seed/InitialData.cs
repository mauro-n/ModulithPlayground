namespace Catalog.Data.Seed
{
    public static class InitialData
    {
        public static List<Product> Products => new List<Product>
        {
            Product.Create(Guid.NewGuid(), "produto 1", new List<string> { "Alfa" }, "", "image/ete", 12.25M),
            Product.Create(Guid.NewGuid(), "produto 2", new List<string> { "Beta" }, "", "image/eee", 65.25M),
        };
    }
}
