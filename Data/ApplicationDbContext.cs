using Microsoft.EntityFrameworkCore;
using Inlämning_Backend.Models;
namespace Inlämning_Backend.Data
{
    public class ApplicationDbContext : DbContext   
    {
        //Takes setting for what database to use.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        //Define tables.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
