namespace Inlämning_Backend.Dtos
{
    public record CartItemDto(int ProductId, int Quantity, string ProductName, decimal Price)
    {
        public decimal Subtotal => Price * Quantity;
    }
}
