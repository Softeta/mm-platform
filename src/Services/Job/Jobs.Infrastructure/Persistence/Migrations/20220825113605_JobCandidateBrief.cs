using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class JobCandidateBrief : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brief",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brief",
                schema: "jobs",
                table: "ArchivedCandidate",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brief",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "Brief",
                schema: "jobs",
                table: "ArchivedCandidate");
        }
    }
}
