using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddAliasToSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AliasTo_Code",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AliasTo_Id",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AliasTo_Code",
                schema: "candidate",
                table: "CandidateSkills",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AliasTo_Id",
                schema: "candidate",
                table: "CandidateSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AliasTo_Code",
                schema: "candidate",
                table: "CandidateDesiredSkills",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AliasTo_Id",
                schema: "candidate",
                table: "CandidateDesiredSkills",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AliasTo_Code",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills");

            migrationBuilder.DropColumn(
                name: "AliasTo_Id",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills");

            migrationBuilder.DropColumn(
                name: "AliasTo_Code",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropColumn(
                name: "AliasTo_Id",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropColumn(
                name: "AliasTo_Code",
                schema: "candidate",
                table: "CandidateDesiredSkills");

            migrationBuilder.DropColumn(
                name: "AliasTo_Id",
                schema: "candidate",
                table: "CandidateDesiredSkills");
        }
    }
}
