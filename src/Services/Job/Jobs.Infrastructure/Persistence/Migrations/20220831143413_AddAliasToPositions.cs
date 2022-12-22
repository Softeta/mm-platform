using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class AddAliasToPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AliasTo_Code",
                schema: "jobs",
                table: "JobSkills",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AliasTo_Id",
                schema: "jobs",
                table: "JobSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "JobContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "JobContactPersons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "JobCandidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "JobCandidates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "ArchivedCandidate",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "ArchivedCandidate",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "AliasTo_Code",
                schema: "jobs",
                table: "JobSkills");

            migrationBuilder.DropColumn(
                name: "AliasTo_Id",
                schema: "jobs",
                table: "JobSkills");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "JobContactPersons");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "JobContactPersons");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "JobCandidates");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "JobCandidates");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Code",
                schema: "jobs",
                table: "ArchivedCandidate");

            migrationBuilder.DropColumn(
                name: "Position_AliasTo_Id",
                schema: "jobs",
                table: "ArchivedCandidate");
        }
    }
}
