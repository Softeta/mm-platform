using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ExtendAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                schema: "candidate",
                table: "Candidates");
        }
    }
}
