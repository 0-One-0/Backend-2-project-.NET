using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Models
{
    [Index(nameof(UrlSlug), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string UrlSlug { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Image { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new();
    }
}
