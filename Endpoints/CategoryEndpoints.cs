using Inlämning_Backend.Dtos;
using Inlämning_Backend.Exceptions;
using Inlämning_Backend.Services;

namespace Inlämning_Backend.Endpoints
{
    public class CategoryEndpoints
    {

        public static void MapCategoryEndpoints(WebApplication app)
        {
            //Get all categories and by slug if sent.
            app.MapGet("/api/categories", async (CategoryService service, string? slug) =>
            {

                if (!string.IsNullOrWhiteSpace(slug))
                {
                    var category = await service.GetCategoriesBySlugAsync(slug);
                    return Results.Ok(category);
                }
                var categories = await service.GetCategoriesAsync();
                return Results.Ok(categories);
            });
            //Get category by id
            app.MapGet("/api/categories/{id:int}", async (CategoryService service, int id) =>
            {
                var category = await service.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(category);
            });
            //Create category
            app.MapPost("/api/categories", async (CategoryService service, CreateCategoryRequest request) =>
            {
                try
                {
                    var category = await service.CreateCategoryAsync(request);
                    return Results.Created($"/api/categories/{category.Id}", category);
                }
                catch (DuplicateSlugException ex)
                {
                    return Results.Conflict(ex.Message);

                }
            });
            //Add product to category by id
            app.MapPost("/api/categories/{categoryID:int}/products/{productId:int}", async (CategoryService service, int categoryID, int productId) =>
            {


                var result = await service.AddProductToCategoryAsync(categoryID, productId);

                if (!result)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();


            });
            //Update category by id
            app.MapPatch("/api/categories/{id:int}", () =>
            {

            });
            //Delete category by id
            app.MapDelete("/api/categories/{id:int}", async (CategoryService service, int id) =>
            {
                var detelet = await service.DeleteCategoryAsync(id);
                if (!detelet)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
            //Take away product from category by id
            app.MapDelete("/api/categories/{categoryID:int}/products/{productId:int}", async (CategoryService service, int categoryID, int productId) =>
            {
                var result = await service.DeleteProductFromCategoryAsync(categoryID, productId);
                if (!result)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();


            });
        }
    }
}
