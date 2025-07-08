using Carter;
using Shared.Extensions;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

            builder.Services
                .AddCatalogModule(builder.Configuration)
                .AddBasketModule()
                .AddOrderingModule();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MapCarter();

            app
                .UseCatalogModule()
                .UseBasketModule()
                .UseOrderingModule();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
