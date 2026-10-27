using Inlämning_Backend.Data;
using Inlämning_Backend.Dtos;
using Inlämning_Backend.Exceptions;
using Inlämning_Backend.Models;
using Inlämning_Backend.Helper;
using Microsoft.EntityFrameworkCore;
namespace Inlämning_Backend.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;
        //Constructor that takes an ApplicationDbContext as a parameter and assigns it to the _context field.
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await _context.Categories.OrderBy(c => c.Id)
                .Select(c => new CategoryDto(
                    c.Id,
                    c.Name,
                    c.Image,
                    c.UrlSlug,
                    c.Products.Select(p => new ProductDto(
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Description,
                        p.Image,
                        p.UrlSlug)).ToList()
                )).ToListAsync();
        }

        public async Task<List<CategoryDto>> GetCategoriesBySlugAsync(string slug)
        {
            return await _context.Categories.Where(c => c.UrlSlug == slug)
                .Select(c => new CategoryDto(
                    c.Id,
                    c.Name,
                    c.Image,
                    c.UrlSlug,
                    c.Products.Select(p => new ProductDto(
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Description,
                        p.Image,
                        p.UrlSlug)).ToList()
                )).ToListAsync();
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Where(c => c.Id == id).Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Image,
                c.UrlSlug,
                c.Products.Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Description,
                    p.Image,
                    p.UrlSlug)).ToList()
            )).FirstOrDefaultAsync();
        }
        public async Task<CategorySummaryDto> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                Image = request.Image,
                UrlSlug = SlugHelper.GenerateSlug(request.Name)
            };
            var exists = await _context.Categories.AnyAsync(c => c.UrlSlug == category.UrlSlug);
            if (exists)
            {
                throw new DuplicateSlugException($"A category with the URL slug '{category.UrlSlug}' already exists.");
            }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new CategorySummaryDto(
                category.Id,
                category.Name,
                category.Image,
                category.UrlSlug
            );
        }
        public async Task AddProductToCategoryAsync(int categoryId, int productId)
        {
            var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }
            if (!category.Products.Contains(product))
            {
                category.Products.Add(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}

                 