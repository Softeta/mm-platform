using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class SplitWorkTypesToValueObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateWorkTypes",
                schema: "candidate");

            migrationBuilder.DropColumn(
                name: "FormatType",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "WorkHours",
                schema: "candidate",
                table: "Candidates",
                newName: "Permanent_WorkHours");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Freelance_EndDate",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Freelance_MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Freelance_MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Freelance_MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Freelance_StartDate",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Freelance_WorkHours",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Freelance_WorkType",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permanent_MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Permanent_MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Permanent_MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Permanent_StartDate",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Permanent_WorkType",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Permanent_WorkingHoursType",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Freelance_EndDate",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Freelance_MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Freelance_MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Freelance_MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Freelance_StartDate",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Freelance_WorkHours",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Freelance_WorkType",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Permanent_MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Permanent_MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Permanent_MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Permanent_StartDate",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Permanent_WorkType",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Permanent_WorkingHoursType",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Permanent_WorkHours",
                schema: "candidate",
                table: "Candidates",
                newName: "WorkHours");

            migrationBuilder.AddColumn<string>(
                name: "FormatType",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CandidateWorkTypes",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinimumSalary_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    MinimumSalary_PerHour = table.Column<decimal>(type: "Decimal(18,2)", nullable: true),
                    MinimumSalary_PerMonth = table.Column<decimal>(type: "Decimal(18,2)", nullable: true),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WorkHours = table.Column<int>(type: "int", nullable: true),
                    WorkType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    WorkingHoursType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateWorkTypes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkTypes_CandidateId",
                schema: "candidate",
                table: "CandidateWorkTypes",
                column: "CandidateId");
        }
    }
}
