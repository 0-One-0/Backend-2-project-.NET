using Inlämning_Backend.Dtos;
using Inlämning_Backend.Exceptions;
using Inlämning_Backend.Services;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Endpoints
{
    public class ProductEndpoints
    {

        public static void MapProductEndpoints(WebApplication app)
        {
            //Get products and also filter by slug, it also takes pagination.
            app.MapGet("/api/products", async (ProductService service, string? slug, int page
                = 1, int pageSize = 10) =>
            {

                if (page < 1 || pageSize < 1 || pageSize > 50)
                {
                    return Results.BadRequest();
                }

                if (!string.IsNullOrWhiteSpace(slug))
                {
                    var product = await service.GetProductBySlugAsync(slug);

                    return Results.Ok(product);
                }
                var products = await service.GetProductsAsync(page, pageSize);
                return Results.Ok(products);

            });
            //Get product by id
            app.MapGet("/api/products/{id:int}", async (ProductService service, int id) =>
            {

                var product = await service.GetProductByIdAsync(id);
                if (product is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(product);

            });
            //Create product
            app.MapPost("/api/products", async (ProductService service, CreateProductRequest request) =>
            {

                try
                {
                    var product = await service.CreateProductAsync(request);
                    return Results.Created($"/api/products/{product.Id}", product);
                }
                catch (DuplicateSlugException ex)
                {
                    return Results.Conflict(ex.Message);
                }

            });
            //Update product by id
            app.MapPatch("/api/products/{id:int}", async (ProductService service, int id, JsonPatchDocument<UpdateProductReq> patch) =>
            {

                var copy = await service.GetProductForUpdateAsync(id);
                if (copy is null)
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
                    var updatedProduct = await service.UpdateProductAsync(id, copy);
                    if (updatedProduct is null)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(updatedProduct);
                }
                catch (DuplicateSlugException ex)
                {
                    return Results.Conflict(ex.Message);
                }

            });
            //Delete product by id
            app.MapDelete("/api/products/{id:int}", async (ProductService service, int id) =>
            {

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
