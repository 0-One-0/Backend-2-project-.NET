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

        public async Task<List<ProductDto>> GetProductsAsync(int page, int pageSize)
        {
            return await _context.Products.OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto(
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

        public async Task<List<ProductDto>> GetProductBySlugAsync(string slug)
        {
            return await _context.Products.Where(p => p.UrlSlug == slug).Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price,
                p.Description,
                p.Image,
                p.UrlSlug)).ToListAsync();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
