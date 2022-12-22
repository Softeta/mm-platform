using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class RenamePropertyToIsJobArchived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsArchived",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                newName: "IsJobArchived");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                newName: "IsJobArchived");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsJobArchived",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "IsJobArchived",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                newName: "IsArchived");
        }
    }
}
