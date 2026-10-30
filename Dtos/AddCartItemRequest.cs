using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Dtos
{
    public record AddCartItemRequest(int ProductId, [Range(1, 15)] int Quantity = 1);
}
