using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddHasAppliedToJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitedAt",
                schema: "candidate",
                table: "CandidateAppliedInJobs");

            migrationBuilder.AddColumn<bool>(
                name: "HasApplied",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasApplied",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasApplied",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "HasApplied",
                schema: "candidate",
                table: "CandidateArchivedInJobs");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InvitedAt",
                schema: "candidate",
                table: "CandidateAppliedInJobs",
                type: "datetimeoffset",
                nullable: true);
        }
    }
}
