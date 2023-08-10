using IdentitySonProje.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentitySonProje.Configuration
{
    public class ExtraCFG : IEntityTypeConfiguration<Extra>
    {
        public void Configure(EntityTypeBuilder<Extra> builder)
        {
            builder.HasData(
                new Extra { ExtraId = 1, Name = "Ketchup", Price = 10, ImageName = "ketchup.jpg" },
                new Extra { ExtraId = 2, Name = "Mayo", Price = 10, ImageName = "mayo.jpg" },
                new Extra { ExtraId = 3, Name = "Mustard", Price = 10, ImageName = "mustard.jpg" },
                new Extra { ExtraId = 4, Name = "Ranch", Price = 10, ImageName = "ranch.jpg" },
                new Extra { ExtraId = 5, Name = "BBQ", Price = 10, ImageName = "bbq.jpg" }
                );
        }
    }
}
