using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ExtendIdsForSkillsIndustriesPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                schema: "jobs",
                table: "Jobs",
                newName: "Position_Code");

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                schema: "jobs",
                table: "JobSkills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Position_Id",
                schema: "jobs",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IndustryId",
                schema: "jobs",
                table: "JobIndustries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId_SkillId",
                schema: "jobs",
                table: "JobSkills",
                columns: new[] { "JobId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobIndustries_JobId_IndustryId",
                schema: "jobs",
                table: "JobIndustries",
                columns: new[] { "JobId", "IndustryId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSkills_JobId_SkillId",
                schema: "jobs",
                table: "JobSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobIndustries_JobId_IndustryId",
                schema: "jobs",
                table: "JobIndustries");

            migrationBuilder.DropColumn(
                name: "SkillId",
                schema: "jobs",
                table: "JobSkills");

            migrationBuilder.DropColumn(
                name: "Position_Id",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                schema: "jobs",
                table: "JobIndustries");

            migrationBuilder.RenameColumn(
                name: "Position_Code",
                schema: "jobs",
                table: "Jobs",
                newName: "Position");
        }
    }
}
