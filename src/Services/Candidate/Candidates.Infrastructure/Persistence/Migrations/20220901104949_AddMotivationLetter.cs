using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddMotivationLetter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivationVideo_FileName",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivationVideo_Uri",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivationVideo_FileName",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivationVideo_Uri",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivationVideo_FileName",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "MotivationVideo_Uri",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "MotivationVideo_FileName",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.DropColumn(
                name: "MotivationVideo_Uri",
                schema: "candidate",
                table: "CandidateArchivedInJobs");
        }
    }
}
