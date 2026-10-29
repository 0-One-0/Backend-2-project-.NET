using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Dtos
{
    public class UpdateCategoryReq
    {
        [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;

        [Required, MaxLength(255)] public string Image { get; set; } = string.Empty;


    }
}
