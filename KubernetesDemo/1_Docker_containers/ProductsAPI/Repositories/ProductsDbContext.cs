using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Repositories
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .ToTable("Product")
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();

            SeedProducts(modelBuilder);
        }

        private void SeedProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Tennis racket",
                    Price = 75.00
                },
                new Product
                {
                    Id = 2,
                    Name = "Hockey stick",
                    Price = 34.06
                },
                new Product
                {
                    Id = 3,
                    Name = "Basketball",
                    Price = 24.99
                },
                new Product
                {
                    Id = 4,
                    Name = "Ice skates",
                    Price = 199.99
                },
                new Product
                {
                    Id = 5,
                    Name = "Baseball glove",
                    Price = 29.37
                }
            );
        }
    }
}
