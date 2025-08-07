using Marketplace.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Storage.Data
{
    public class MarketplaceContext : IdentityDbContext<ApplicationUser>
    {
        public MarketplaceContext(DbContextOptions<MarketplaceContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
            .HasMany(t => t.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .IsRequired(false);

            modelBuilder.Entity<Product>()
            .HasMany(t => t.Categories);

            modelBuilder.Entity<Company>()
            .HasMany(c => c.Users);
        }
    }
}
