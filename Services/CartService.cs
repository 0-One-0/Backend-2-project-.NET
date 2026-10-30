using Inlämning_Backend.Data;
using Inlämning_Backend.Dtos;
using Inlämning_Backend.Exceptions;
using Inlämning_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Inlämning_Backend.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        //Constructor that takes an ApplicationDbContext as a parameter and assigns it to the _context field.
        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto> CreateCartAsync()
        {
            var cart = new Cart();
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return new CartDto(cart.Id, new List<CartItemDto>());
        }

        public async Task<CartDto?> GetCartByIdAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null) return null;
            var items = await _context.CartItems
                .Where(ci => ci.CartId == cartId)
                .Select(ci => new CartItemDto(
                    ci.ProductId,
                    ci.Quantity,
                    ci.Product.Name,
                    ci.Product.Price))
                .ToListAsync();
            return new CartDto(cart.Id, items);
        }

        public async Task<CartItemDto?> AddCartItemAsync(int cartId, AddCartItemRequest request)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null) return null;
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null) return null;
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == request.ProductId);
            if (cartItem != null)
            {
                if (cartItem.Quantity + request.Quantity > 15)
                {
                    throw new MaxQuantityExceededException("Maximum quantity allowed is 15.");
                }

                cartItem.Quantity += request.Quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                _context.CartItems.Add(cartItem);
            }
            await _context.SaveChangesAsync();
            return new CartItemDto(cartItem.ProductId, cartItem.Quantity, product.Name, product.Price);
        }

        public async Task<CartItemDto?> UpdateCartItemAsync(int cartId, int productId, UpdateCartItemRequest request)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (cartItem == null) return null;
            cartItem.Quantity = request.Quantity;
            await _context.SaveChangesAsync();
            var product = await _context.Products.FindAsync(productId);
            return new CartItemDto(cartItem.ProductId, cartItem.Quantity, product!.Name, product.Price);
        }

        public async Task<bool> DeleteCartAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null) return false;
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCartItemAsync(int cartId, int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (cartItem == null) return false;
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
