using Inlämning_Backend.Services;

namespace Inlämning_Backend.Endpoints
{
    public class ProductEndpoints
    {
        
        public static void MapProductEndpoints(WebApplication app)
        {
            //Get products and also filter by slug, it also takes pagination.
            app.MapGet("api/products", async (ProductService service, string? slug, int page 
                = 1, int pageSize = 10) => {

                    if (page < 1 || pageSize < 1 || pageSize > 50)
                    {
                        return Results.BadRequest();
                    }

                if(slug is not null)
                {
                    var product = await service.GetProductBySlugAsync(slug);
                
                    return Results.Ok(product);
                }
                var products = await service.GetProductsAsync(page, pageSize);
                return Results.Ok(products);

            });
            //Get product by id
            app.MapGet("api/products/{id:int}", async (ProductService service, int id) => {

                var product = await service.GetProductByIdAsync(id);
                if (product is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(product);

            });
            //Create product
            app.MapPost("api/products", () => {

            });
            //Update product by id
            app.MapPatch("api/products/{id:int}", () => {

            });
            //Delete product by id
            app.MapDelete("api/products/{id:int}", async (ProductService service, int id) => {

               var res = await service.DeleteProductAsync(id);

                if (!res)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }
    }
    
}
