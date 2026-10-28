using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Dtos
{
    public class UpdateProductReq
    {


        [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
        [Required, Range(1, 100000)] public decimal Price { get; set; }
        [Required, MaxLength(255)] public string Image { get; set; } = string.Empty;

        [Required, MaxLength(1000)] public string Description { get; set; } = string.Empty;

    }
}
