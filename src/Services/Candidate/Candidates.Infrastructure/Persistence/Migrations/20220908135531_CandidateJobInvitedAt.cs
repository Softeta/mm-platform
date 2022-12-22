using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class CandidateJobInvitedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InvitedAt",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InvitedAt",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitedAt",
                schema: "candidate",
                table: "CandidateSelectedInJobs");

            migrationBuilder.DropColumn(
                name: "InvitedAt",
                schema: "candidate",
                table: "CandidateArchivedInJobs");
        }
    }
}
