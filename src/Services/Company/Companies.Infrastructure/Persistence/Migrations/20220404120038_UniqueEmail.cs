using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class UniqueEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_CompanyId_Email",
                schema: "companies",
                table: "ContactPersons",
                columns: new[] { "CompanyId", "Email" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_CompanyId_Email",
                schema: "companies",
                table: "ContactPersons");
        }
    }
}
