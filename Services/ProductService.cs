using Inlämning_Backend.Data;
using Inlämning_Backend.Dtos;
using Inlämning_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Inlämning_Backend.Services
{
    public class ProductService
    {
        //Link to the database context, which is used to interact with the database.
        private readonly ApplicationDbContext _context;
        //Constructor that takes an ApplicationDbContext as a parameter and assigns it to the _context field.
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            return await _context.Products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price,
                p.Description,
                p.Image,
                p.UrlSlug)).ToListAsync();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Where(p => p.Id == id).Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price,
                p.Description,
                p.Image,
                p.UrlSlug)).FirstOrDefaultAsync();
        }
    }
}
