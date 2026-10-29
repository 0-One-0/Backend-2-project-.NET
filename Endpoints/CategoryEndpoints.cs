using Inlämning_Backend.Dtos;
using Inlämning_Backend.Exceptions;
using Inlämning_Backend.Services;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson.Exceptions;
using System.ComponentModel.DataAnnotations;

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
            app.MapPatch("/api/categories/{id:int}", async (CategoryService service, int id, JsonPatchDocument<UpdateCategoryReq> patch) =>

            {
                var copy = await service.GetCategoryForUpdateAsync(id);
                if (copy == null)
                {
                    return Results.NotFound();
                }
                try
                {
                    patch.ApplyTo(copy);
                }
                catch (JsonPatchException ex)
                {
                    return Results.BadRequest(ex.Message);
                }


                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(copy, new ValidationContext(copy), results, validateAllProperties: true);

                if (!isValid)
                {
                    var errors = results
                        .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
                        .ToDictionary(g => g.Key, g => g.Select(r => r.ErrorMessage ?? "").ToArray());

                    return Results.ValidationProblem(errors);
                }
                try
                {
                    var updatedCategory = await service.UpdateCategoryAsync(id, copy);
                    if (updatedCategory is null)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(updatedCategory);
                }
                catch (DuplicateSlugException ex)
                {
                    return Results.Conflict(ex.Message);
                }


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
