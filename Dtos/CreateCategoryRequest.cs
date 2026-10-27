using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Dtos
{
    public record CreateCategoryRequest([Required, MaxLength(100)] string Name, [Required, MaxLength(255)] string Image);
    
}
