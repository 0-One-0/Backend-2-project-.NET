using Inlämning_Backend.Dtos;
using Inlämning_Backend.Exceptions;
using Inlämning_Backend.Services;
namespace Inlämning_Backend.Endpoints
{
    public class CartEndpoints
    {
        public static void MapCartEndpoints(WebApplication app)
        {
            //Create a new cart
            app.MapPost("/api/carts", async (CartService service) =>
            {
                var cart = await service.CreateCartAsync();
                return Results.Created($"/api/carts/{cart.Id}", cart);
            });
            //Get cart by id
            app.MapGet("/api/carts/{CartId:int}", async (CartService service, int CartId) =>
            {
                var cart = await service.GetCartByIdAsync(CartId);
                if (cart is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(cart);
            });
            //Add an item to the cart by id
            app.MapPost("/api/carts/{CartId:int}/items", async (CartService service, int CartId, AddCartItemRequest request) =>
            {
                try
                {
                    var cartItem = await service.AddCartItemAsync(CartId, request);
                    if (cartItem is null)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(cartItem);
                }
                catch (MaxQuantityExceededException ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            });
            //Update cart item by id and productId
            app.MapPut("/api/carts/{CartId:int}/items/{ProductId:int}", async (CartService service, int CartId, int ProductId, UpdateCartItemRequest request) =>
            {


                var cartItem = await service.UpdateCartItemAsync(CartId, ProductId, request);
                if (cartItem is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(cartItem);


            });
            //Delete cart by id
            app.MapDelete("/api/carts/{CartId:int}", async (CartService service, int CartId) =>
            {
                var deleted = await service.DeleteCartAsync(CartId);
                if (!deleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });

            //Delete an item from the cart by id and productId
            app.MapDelete("/api/carts/{CartId:int}/items/{ProductId:int}", async (CartService service, int CartId, int ProductId) =>
            {
                var deleted = await service.DeleteCartItemAsync(CartId, ProductId);
                if (!deleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });



        }
    }
}
