using Inlämning_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Dtos
{
    public record CategoryDto(int Id, string Name, string Image, string UrlSlug, List<ProductDto> Products);
}
