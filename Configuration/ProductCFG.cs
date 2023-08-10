using IdentitySonProje.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentitySonProje.Configuration
{
    public class ProductCFG : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { ProductId = 1, Name = "Cheese Burger", Price = 100, ImageName = "cheese_burger.jpg", CategoryId = 1 },
                new Product { ProductId = 2, Name = "Chicken Burger", Price = 120, ImageName = "chicken_burger.jpg", CategoryId = 1 },
                new Product { ProductId = 3, Name = "Mushroom Burger", Price = 140, ImageName = "mushroom_burger.jpg", CategoryId = 1 },
                new Product { ProductId = 4, Name = "Beef Burger", Price = 160, ImageName = "beef_burger.jpg", CategoryId = 1 },
                new Product { ProductId = 5, Name = "Coke", Price = 20, ImageName = "coke.jpg", CategoryId = 2 },
                new Product { ProductId = 6, Name = "Beer", Price = 70, ImageName = "beer.jpg", CategoryId = 2 }
                );
        }
    }
}
