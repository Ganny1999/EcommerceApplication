using EcommerceCustomerModule.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EcommerceCustomerModule.DataContext
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresss { get; set; }
    }
}
