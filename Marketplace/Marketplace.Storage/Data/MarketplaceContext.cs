using Marketplace.Core.Helpers;
using Marketplace.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<Merchant> Merchants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasMany(t => t.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .IsRequired(false);

            modelBuilder.Entity<Product>()
            .HasMany(t => t.Categories);

            // Всегда вызывайте базовый метод OnModelCreating!
            base.OnModelCreating(modelBuilder);

            // --- Заполнение начальных данных для ролей (Seed Data) ---
            // Это добавит предопределенные роли в вашу базу данных при первой миграции.
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "client-role-id", Name = "Client", NormalizedName = "CLIENT" },
                new IdentityRole { Id = "manager-role-id", Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Id = "admin-role-id", Name = "Admin", NormalizedName = "ADMIN" }
            );

            // --- Заполнение начальных данных для тестового администратора (ТОЛЬКО ДЛЯ РАЗРАБОТКИ!) ---
            // Это создаст тестового пользователя с паролем, которого вы сможете использовать для входа.
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(), // Уникальный GUID для Id пользователя
                UserName = "admin@yourmarketplace.com",
                NormalizedUserName = "ADMIN@YOURMARKETPLACE.COM",
                Email = "admin@yourmarketplace.com",
                NormalizedEmail = "ADMIN@YOURMARKETPLACE.COM",
                EmailConfirmed = true, // Считаем email подтвержденным для тестового пользователя
                PasswordHash = hasher.HashPassword(null, "Test"), // <<<<< ИЗМЕНИТЕ ЭТОТ ПАРОЛЬ!
                SecurityStamp = Guid.NewGuid().ToString(),
                Nickname = "SuperAdmin",
                Description = "Главный администратор системы",
                Status = OnlineStatus.Online,
                CreatedAt = DateTime.UtcNow
            };

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // --- Назначение ролей тестовому администратору ---
            // Связываем тестового пользователя с ролями "Admin" и "Manager"
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = "admin-role-id" },
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = "manager-role-id" }
            );
        }
    }
}
