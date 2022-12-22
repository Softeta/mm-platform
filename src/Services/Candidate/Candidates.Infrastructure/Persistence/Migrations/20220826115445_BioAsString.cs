using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class BioAsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio_FileName",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Bio_Uri",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.AddColumn<string>(
                name: "Bio_FileName",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio_Uri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }
    }
}
