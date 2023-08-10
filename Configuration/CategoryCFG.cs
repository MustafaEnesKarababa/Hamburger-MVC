using IdentitySonProje.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentitySonProje.Configuration
{
    public class CategoryCFG : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { CategoryId = 1, Name = "Burgers" },
                new Category { CategoryId = 2, Name = "Drinks" }
                );
        }
    }
}
