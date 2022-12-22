using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class AddHasAppliedToJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasApplied",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasApplied",
                schema: "jobs",
                table: "ArchivedCandidate",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasApplied",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "HasApplied",
                schema: "jobs",
                table: "ArchivedCandidate");
        }
    }
}
