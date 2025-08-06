using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Storage.Migrations
{
    /// <inheritdoc />
    public partial class CompanyUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyPassword",
                table: "Companies",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "CompanyEmail",
                table: "Companies",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Companies",
                newName: "CompanyPassword");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Companies",
                newName: "CompanyEmail");
        }
    }
}
