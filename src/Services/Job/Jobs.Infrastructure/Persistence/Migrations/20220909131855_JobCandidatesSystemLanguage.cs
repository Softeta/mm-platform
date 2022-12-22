using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class JobCandidatesSystemLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemLanguage",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemLanguage",
                schema: "jobs",
                table: "ArchivedCandidate",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemLanguage",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "SystemLanguage",
                schema: "jobs",
                table: "ArchivedCandidate");
        }
    }
}
