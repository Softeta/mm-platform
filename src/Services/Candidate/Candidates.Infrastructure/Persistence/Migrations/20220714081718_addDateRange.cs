using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class addDateRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                newName: "Period_To");

            migrationBuilder.RenameColumn(
                name: "From",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                newName: "Period_From");

            migrationBuilder.RenameColumn(
                name: "Terms_StartDate",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Availability_To");

            migrationBuilder.RenameColumn(
                name: "Terms_EndDate",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Availability_From");

            migrationBuilder.RenameColumn(
                name: "To",
                schema: "candidate",
                table: "CandidateEducations",
                newName: "Period_To");

            migrationBuilder.RenameColumn(
                name: "From",
                schema: "candidate",
                table: "CandidateEducations",
                newName: "Period_From");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Period_To",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "Period_From",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                newName: "From");

            migrationBuilder.RenameColumn(
                name: "Terms_Availability_To",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_StartDate");

            migrationBuilder.RenameColumn(
                name: "Terms_Availability_From",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_EndDate");

            migrationBuilder.RenameColumn(
                name: "Period_To",
                schema: "candidate",
                table: "CandidateEducations",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "Period_From",
                schema: "candidate",
                table: "CandidateEducations",
                newName: "From");
        }
    }
}
