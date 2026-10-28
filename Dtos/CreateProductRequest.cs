using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Dtos
{
    public record CreateProductRequest(
        [Required, MaxLength(100)] string Name,
        [Required, Range(1, 100000)] decimal Price,
        [Required, MaxLength(1000)] string Description,
        [Required, MaxLength(255)] string Image);
}
