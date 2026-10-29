using Microsoft.EntityFrameworkCore;

namespace Inlämning_Backend.Models
{
    [Index(nameof(CartId), nameof(ProductId), IsUnique = true)]
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
