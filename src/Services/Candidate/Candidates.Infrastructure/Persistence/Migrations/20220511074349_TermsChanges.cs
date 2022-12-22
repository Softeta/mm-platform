using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class TermsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateJobFormats",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateWorkingHours",
                schema: "candidate");

            migrationBuilder.DropColumn(
                name: "Freelance_MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Freelance_MinimumSalary_PerHour",
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

            migrationBuilder.RenameColumn(
                name: "Permanent_WorkType",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Permanent_WorkType");

            migrationBuilder.RenameColumn(
                name: "Permanent_StartDate",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_StartDate");

            migrationBuilder.RenameColumn(
                name: "Permanent_MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Currency");

            migrationBuilder.RenameColumn(
                name: "Freelance_WorkType",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Freelance_WorkType");

            migrationBuilder.RenameColumn(
                name: "Freelance_EndDate",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_EndDate");

            migrationBuilder.RenameColumn(
                name: "Permanent_WorkHours",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_PartTimeWorkingHours_Weekly");

            migrationBuilder.RenameColumn(
                name: "Permanent_MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Permanent_MonthlySalary");

            migrationBuilder.RenameColumn(
                name: "Permanent_MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Freelance_MonthlySalary");

            migrationBuilder.RenameColumn(
                name: "Freelance_MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates",
                newName: "Terms_Freelance_HourlySalary");

            migrationBuilder.AlterColumn<string>(
                name: "Terms_Currency",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Terms_Formats_IsHybrid",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Terms_Formats_IsOnSite",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Terms_Formats_IsRemote",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Terms_FulTimeWorkingHours_Type",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Terms_PartTimeWorkingHours_Type",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Terms_Formats_IsHybrid",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Terms_Formats_IsOnSite",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Terms_Formats_IsRemote",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Terms_FulTimeWorkingHours_Type",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Terms_PartTimeWorkingHours_Type",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Terms_StartDate",
                schema: "candidate",
                table: "Candidates",
                newName: "Permanent_StartDate");

            migrationBuilder.RenameColumn(
                name: "Terms_Permanent_WorkType",
                schema: "candidate",
                table: "Candidates",
                newName: "Permanent_WorkType");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_WorkType",
                schema: "candidate",
                table: "Candidates",
                newName: "Freelance_WorkType");

            migrationBuilder.RenameColumn(
                name: "Terms_EndDate",
                schema: "candidate",
                table: "Candidates",
                newName: "Freelance_EndDate");

            migrationBuilder.RenameColumn(
                name: "Terms_Currency",
                schema: "candidate",
                table: "Candidates",
                newName: "Permanent_MinimumSalary_Currency");

            migrationBuilder.RenameColumn(
                name: "Terms_Permanent_MonthlySalary",
                schema: "candidate",
                table: "Candidates",
                newName: "Permanent_MinimumSalary_PerMonth");

            migrationBuilder.RenameColumn(
                name: "Terms_PartTimeWorkingHours_Weekly",
                schema: "candidate",
                table: "Candidates",
                newName: "Permanent_WorkHours");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_MonthlySalary",
                schema: "candidate",
                table: "Candidates",
                newName: "Permanent_MinimumSalary_PerHour");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_HourlySalary",
                schema: "candidate",
                table: "Candidates",
                newName: "Freelance_MinimumSalary_PerMonth");

            migrationBuilder.AlterColumn<string>(
                name: "Permanent_MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateTable(
                name: "CandidateJobFormats",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FormatType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateJobFormats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateJobFormats_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateWorkingHours",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    WorkingHoursType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateWorkingHours_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateJobFormats_CandidateId",
                schema: "candidate",
                table: "CandidateJobFormats",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkingHours_CandidateId",
                schema: "candidate",
                table: "CandidateWorkingHours",
                column: "CandidateId");
        }
    }
}
