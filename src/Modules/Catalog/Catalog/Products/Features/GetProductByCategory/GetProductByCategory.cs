﻿namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);
    public class GetProductByCategory : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            })
               .WithName("GetProductByCategory")
               .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithSummary("Get Product By Category")
               .WithDescription("Get Product By Category");
        }
    }
}
