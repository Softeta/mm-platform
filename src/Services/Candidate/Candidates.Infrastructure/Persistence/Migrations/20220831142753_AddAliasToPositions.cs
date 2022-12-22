using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddAliasToPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentPosition_AliasTo_Code",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentPosition_AliasTo_Id",
                schema: "candidate",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "candidate",
                table: "CandidateWorkExperiences");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "candidate",
                table: "CandidateWorkExperiences");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "CurrentPosition_AliasTo_Code",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CurrentPosition_AliasTo_Id",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "candidate",
                table: "CandidateArchivedInJobs");
        }
    }
}
