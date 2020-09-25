using Microsoft.EntityFrameworkCore;
using OrdersAPI.Models;

namespace OrdersAPI.Repositories
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                        .ToTable("Order")
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();

        }
    }
}
