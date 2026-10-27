using Inlämning_Backend.Data;
using Inlämning_Backend.Dtos;
using Inlämning_Backend.Exceptions;
using Inlämning_Backend.Models;
using Inlämning_Backend.Helper;
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

        public async Task<ProductDto> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Image = request.Image,
                UrlSlug = SlugHelper.GenerateSlug(request.Name)
            };
           
            var exists = await _context.Products.AnyAsync(p => p.UrlSlug == product.UrlSlug);
            if (exists)
            {
                throw new DuplicateSlugException($"A product with the name '{request.Name}' already exists.");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return new ProductDto(
                product.Id,
                product.Name,
                product.Price,
                product.Description,
                product.Image,
                product.UrlSlug);
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
