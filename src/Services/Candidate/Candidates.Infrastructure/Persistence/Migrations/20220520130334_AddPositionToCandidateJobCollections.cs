using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddPositionToCandidateJobCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateShortListedJobs_Candidates_CandidateId",
                schema: "candidate",
                table: "CandidateShortListedJobs");

            migrationBuilder.DropIndex(
                name: "IX_CandidateShortListedJobs_CandidateId",
                schema: "candidate",
                table: "CandidateShortListedJobs");

            migrationBuilder.RenameTable(
                name: "CandidateShortListedJobs",
                schema: "candidate",
                newName: "CandidateShortListedJobs");

            migrationBuilder.AlterColumn<string>(
                name: "JobPosition",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsShortListed",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "JobPosition",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShortListed",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameTable(
                name: "CandidateShortListedJobs",
                newName: "CandidateShortListedJobs",
                newSchema: "candidate");

            migrationBuilder.AlterColumn<string>(
                name: "JobPosition",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "JobPosition",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateShortListedJobs_CandidateId",
                schema: "candidate",
                table: "CandidateShortListedJobs",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateShortListedJobs_Candidates_CandidateId",
                schema: "candidate",
                table: "CandidateShortListedJobs",
                column: "CandidateId",
                principalSchema: "candidate",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
