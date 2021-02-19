using Microsoft.EntityFrameworkCore;
using Wsei_web.Models;


namespace Wsei_web.Database
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // fluent configuration ...
        }
    }
}