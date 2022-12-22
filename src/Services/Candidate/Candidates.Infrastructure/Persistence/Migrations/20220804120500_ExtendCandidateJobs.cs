using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ExtendCandidateJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company_LogoUri",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeadlineDate",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Freelance",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permanent",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company_LogoUri",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeadlineDate",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Freelance",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permanent",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_LogoUri",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "Freelance",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "Permanent",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "Company_LogoUri",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.DropColumn(
                name: "Freelance",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.DropColumn(
                name: "Permanent",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "candidate",
                table: "CandidateArchivedInJobs");
        }
    }
}
