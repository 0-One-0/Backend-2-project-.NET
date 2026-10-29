namespace Inlämning_Backend.Endpoints
{
    public class CartEndpoints
    {
        public static void MapCartEndpoints(WebApplication app)
        {
            //Create a new cart
            app.MapPost("/api/carts", async (CartService service) =>
            {
                return Results.Ok();
            });
            //Get cart by id
            app.MapGet("/api/carts/{CartId:int}", async (CartService service, int CartId) =>
            {
                return Results.Ok();
            });
            //Add an item to the cart by id
            app.MapPost("/api/carts/{CartId:int}/items", async (CartService service, int CartId, AddCartItemRequest request) =>
            {
                return Results.Ok();
            });
            //Update cart item by id and productId
            app.MapPut("/api/carts/{CartId:int}/items/{ProductId:int}", async (CartService service, int CartId, int ProductId, UpdateCartItemRequest request) =>
            {
                return Results.Ok();
            });
            //Delete cart by id
            app.MapDelete("/api/carts/{CartId:int}", async (CartService service, int CartId) =>
            {
                return Results.Ok();
            });

            //Delete an item from the cart by id and productId
            app.MapDelete("/api/carts/{CartId:int}/items/{ProductId:int}", async (CartService service, int CartId, int ProductId) =>
            {
                return Results.Ok();
            });

        }
    }
}
