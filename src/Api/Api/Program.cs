namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddCatalogModule()
                .AddBasketModule()
                .AddOrderingModule();

            var app = builder.Build();

            app
                .UseCatalogModule()
                .UseBasketModule()
                .UseOrderingModule();

            app.Run();
        }
    }
}
