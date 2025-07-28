using EcommerceOrderModule.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceOrderModule.DataContext
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
