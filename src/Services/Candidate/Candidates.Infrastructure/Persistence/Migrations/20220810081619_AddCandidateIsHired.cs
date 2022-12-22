using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddCandidateIsHired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHired",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHired",
                schema: "candidate",
                table: "CandidateJobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHired",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "IsHired",
                schema: "candidate",
                table: "CandidateJobs");
        }
    }
}
