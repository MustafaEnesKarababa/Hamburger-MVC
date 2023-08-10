using IdentitySonProje.Configuration;
using IdentitySonProje.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentitySonProje.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Extra> Extras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = 2, Name = "User", NormalizedName = "USER" }
                );

            builder.ApplyConfiguration<Category>(new CategoryCFG());
            builder.ApplyConfiguration<Extra>(new ExtraCFG());
            builder.ApplyConfiguration<Product>(new ProductCFG());

            base.OnModelCreating(builder);
        }
    }
}