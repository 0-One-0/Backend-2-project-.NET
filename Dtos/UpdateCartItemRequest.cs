using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Dtos
{
    public record UpdateCartItemRequest([Range(1, 15)] int Quantity);
}
