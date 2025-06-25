using EcommerceCartModule.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceCartModule.DataContext
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {                
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
