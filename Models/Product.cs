using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Inlämning_Backend.Models
{
    [Index(nameof(UrlSlug), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Precision(10, 2)]
        public decimal Price { get; set; }
        [MaxLength(255)]
        public string Image { get; set; } = string.Empty;
        [MaxLength(100)]
        public string UrlSlug { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = new();
    }
}
