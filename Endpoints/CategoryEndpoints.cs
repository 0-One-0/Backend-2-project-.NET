namespace Inlämning_Backend.Endpoints
{
    public class CategoryEndpoints
    {

        public static void MapCategoryEndpoints(WebApplication app)
        {
            //Get all categories and by slug if sent.
            app.MapGet("api/categories", () =>
            {
            });
            //Get category by id
            app.MapGet("api/categories/{id:int}", () =>
            {
            });
            //Create category
            app.MapPost("api/categories", () =>
            {
            });
            //Add product to category by id
            app.MapPost("api/categories/{categoryID:int}/products/{productId:int}", () =>
            {
            });
            //Update category by id
            app.MapPatch("api/categories/{id:int}", () =>
            {
            });
            //Delete category by id
            app.MapDelete("api/categories/{id:int}", () =>
            {
            });
            //Take away product from category by id
            app.MapDelete("/api/categories/{categoryID:int}/products/{productId:int}", () =>
            {
            });
        }
    }
}
