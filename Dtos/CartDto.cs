namespace Inlämning_Backend.Dtos
{
    public record CartDto(int Id, List<CartItemDto> CartItems)
    {
        public decimal Total => CartItems.Sum(item => item.Subtotal);
    }
}
