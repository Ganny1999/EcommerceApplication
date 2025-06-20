using EcommerceProductModule.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProductModule.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {                
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
