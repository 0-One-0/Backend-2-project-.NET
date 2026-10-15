namespace Inlämning_Backend.Endpoints
{
    public class ProductEndpoints
    {
        
        public static void MapProductEndpoints(WebApplication app)
        {
            //Get products and also filter by slug, it also takes pagination.
            app.MapGet("api/products", ()=> { 
            
            });
            //Get product by id
            app.MapGet("api/products/{id:int}", ()=> { 
            
            });
            //Create product
            app.MapPost("api/products", () => {

            });
            //Update product by id
            app.MapPatch("api/products/{id:int}", () => {

            });
            //Delete product by id
            app.MapDelete("api/products/{id:int}", () => {

            });
        }
    }
    
}
