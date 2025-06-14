using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Marketplace.Storage.Migrations
{
    /// <inheritdoc />
    public partial class Secondary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Merchant_MerchantId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "admin-role-id", "978ad6d1-c88a-4af8-88bb-9eb9473dd4b8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "manager-role-id", "978ad6d1-c88a-4af8-88bb-9eb9473dd4b8" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "978ad6d1-c88a-4af8-88bb-9eb9473dd4b8");

            migrationBuilder.RenameTable(
                name: "Merchant",
                newName: "Merchants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TaxNumber = table.Column<string>(type: "text", nullable: false),
                    RegNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Merchants_MerchantId",
                table: "Products",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Merchants_MerchantId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants");

            migrationBuilder.RenameTable(
                name: "Merchants",
                newName: "Merchant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "978ad6d1-c88a-4af8-88bb-9eb9473dd4b8", 0, "b8160920-6d72-4453-9608-daac3232b242", new DateTime(2025, 6, 9, 19, 57, 57, 669, DateTimeKind.Utc).AddTicks(2023), "Главный администратор системы", "admin@yourmarketplace.com", true, false, null, "SuperAdmin", "ADMIN@YOURMARKETPLACE.COM", "ADMIN@YOURMARKETPLACE.COM", "AQAAAAIAAYagAAAAENJbSO7gV0lO1Hm8wsIOahXsJhNrwk+3efw0baVcbvvJtrzB3FBp1cf6FRUele1b5Q==", null, false, null, "2731ddd4-4998-4c89-897b-672a50d10096", 1, false, "admin@yourmarketplace.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "admin-role-id", "978ad6d1-c88a-4af8-88bb-9eb9473dd4b8" },
                    { "manager-role-id", "978ad6d1-c88a-4af8-88bb-9eb9473dd4b8" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Merchant_MerchantId",
                table: "Products",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id");
        }
    }
}
