using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ExtendJobCandidates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeadlineDate",
                schema: "jobs",
                table: "JobCandidates",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Freelance",
                schema: "jobs",
                table: "JobCandidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permanent",
                schema: "jobs",
                table: "JobCandidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "jobs",
                table: "JobCandidates",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                schema: "jobs",
                table: "JobCandidates");

            migrationBuilder.DropColumn(
                name: "Freelance",
                schema: "jobs",
                table: "JobCandidates");

            migrationBuilder.DropColumn(
                name: "Permanent",
                schema: "jobs",
                table: "JobCandidates");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "jobs",
                table: "JobCandidates");
        }
    }
}
