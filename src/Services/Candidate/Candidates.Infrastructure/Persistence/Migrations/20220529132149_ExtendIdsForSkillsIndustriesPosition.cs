using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ExtendIdsForSkillsIndustriesPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkills_Candidates_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkills_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropColumn(
                name: "CandidateId1",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.RenameColumn(
                name: "Position",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                newName: "Position_Code");

            migrationBuilder.RenameColumn(
                name: "JobPosition",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                newName: "Position_Code");

            migrationBuilder.RenameColumn(
                name: "CurrentPosition",
                schema: "candidate",
                table: "Candidates",
                newName: "CurrentPosition_Code");

            migrationBuilder.RenameColumn(
                name: "JobPosition",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                newName: "Position_Code");

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Position_Id",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                schema: "candidate",
                table: "CandidateSkills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Position_Id",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentPosition_Id",
                schema: "candidate",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IndustryId",
                schema: "candidate",
                table: "CandidateIndustries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Position_Id",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CandidateDesiredSkills",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateDesiredSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateDesiredSkills_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkExperienceSkills_CandidateWorkExperienceId_SkillId",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills",
                columns: new[] { "CandidateWorkExperienceId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_CandidateId_SkillId",
                schema: "candidate",
                table: "CandidateSkills",
                columns: new[] { "CandidateId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateIndustries_CandidateId_IndustryId",
                schema: "candidate",
                table: "CandidateIndustries",
                columns: new[] { "CandidateId", "IndustryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDesiredSkills_CandidateId",
                schema: "candidate",
                table: "CandidateDesiredSkills",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDesiredSkills_CandidateId_SkillId",
                schema: "candidate",
                table: "CandidateDesiredSkills",
                columns: new[] { "CandidateId", "SkillId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateDesiredSkills",
                schema: "candidate");

            migrationBuilder.DropIndex(
                name: "IX_CandidateWorkExperienceSkills_CandidateWorkExperienceId_SkillId",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkills_CandidateId_SkillId",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropIndex(
                name: "IX_CandidateIndustries_CandidateId_IndustryId",
                schema: "candidate",
                table: "CandidateIndustries");

            migrationBuilder.DropColumn(
                name: "SkillId",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills");

            migrationBuilder.DropColumn(
                name: "Position_Id",
                schema: "candidate",
                table: "CandidateWorkExperiences");

            migrationBuilder.DropColumn(
                name: "SkillId",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropColumn(
                name: "Position_Id",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "CurrentPosition_Id",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                schema: "candidate",
                table: "CandidateIndustries");

            migrationBuilder.DropColumn(
                name: "Position_Id",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.RenameColumn(
                name: "Position_Code",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "Position_Code",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                newName: "JobPosition");

            migrationBuilder.RenameColumn(
                name: "CurrentPosition_Code",
                schema: "candidate",
                table: "Candidates",
                newName: "CurrentPosition");

            migrationBuilder.RenameColumn(
                name: "Position_Code",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                newName: "JobPosition");

            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId1",
                schema: "candidate",
                table: "CandidateSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills",
                column: "CandidateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkills_Candidates_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills",
                column: "CandidateId1",
                principalSchema: "candidate",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
