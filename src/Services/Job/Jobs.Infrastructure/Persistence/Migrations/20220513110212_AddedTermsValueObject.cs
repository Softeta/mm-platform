using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class AddedTermsValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobFormats",
                schema: "jobs");

            migrationBuilder.DropTable(
                name: "JobWorkingHours",
                schema: "jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_SalaryBudget_Currency",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_SalaryBudget_PerHour_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_SalaryBudget_PerHour_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_StartDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Permanent_WorkType",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Permanent_WorkType");

            migrationBuilder.RenameColumn(
                name: "Permanent_StartDate",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_StartDate");

            migrationBuilder.RenameColumn(
                name: "Permanent_SalaryBudget_PerMonth_To",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Permanent_MonthlyBudget_To");

            migrationBuilder.RenameColumn(
                name: "Permanent_SalaryBudget_PerMonth_From",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Permanent_MonthlyBudget_From");

            migrationBuilder.RenameColumn(
                name: "Permanent_SalaryBudget_PerHour_To",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Freelance_MonthlyBudget_To");

            migrationBuilder.RenameColumn(
                name: "Permanent_SalaryBudget_PerHour_From",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Freelance_MonthlyBudget_From");

            migrationBuilder.RenameColumn(
                name: "Permanent_SalaryBudget_Currency",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Currency");

            migrationBuilder.RenameColumn(
                name: "Freelance_WorkType",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Freelance_WorkType");

            migrationBuilder.RenameColumn(
                name: "Freelance_SalaryBudget_PerMonth_To",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Freelance_HourlyBudget_To");

            migrationBuilder.RenameColumn(
                name: "Freelance_SalaryBudget_PerMonth_From",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Freelance_HourlyBudget_From");

            migrationBuilder.RenameColumn(
                name: "Freelance_HoursPerProject",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Freelance_HoursPerProject");

            migrationBuilder.RenameColumn(
                name: "Freelance_EndDate",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_EndDate");

            migrationBuilder.RenameColumn(
                name: "Permanent_MonthlyHours",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_PartTimeWorkingHours_Weekly");

            migrationBuilder.AddColumn<bool>(
                name: "Terms_Formats_IsHybrid",
                schema: "jobs",
                table: "Jobs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Terms_Formats_IsOnSite",
                schema: "jobs",
                table: "Jobs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Terms_Formats_IsRemote",
                schema: "jobs",
                table: "Jobs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Terms_FullTimeWorkingHours_Type",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Terms_PartTimeWorkingHours_Type",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Terms_Formats_IsHybrid",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Terms_Formats_IsOnSite",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Terms_Formats_IsRemote",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Terms_FullTimeWorkingHours_Type",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Terms_PartTimeWorkingHours_Type",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Terms_StartDate",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_StartDate");

            migrationBuilder.RenameColumn(
                name: "Terms_Permanent_WorkType",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_WorkType");

            migrationBuilder.RenameColumn(
                name: "Terms_Permanent_MonthlyBudget_To",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_SalaryBudget_PerMonth_To");

            migrationBuilder.RenameColumn(
                name: "Terms_Permanent_MonthlyBudget_From",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_SalaryBudget_PerMonth_From");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_WorkType",
                schema: "jobs",
                table: "Jobs",
                newName: "Freelance_WorkType");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_MonthlyBudget_To",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_SalaryBudget_PerHour_To");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_MonthlyBudget_From",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_SalaryBudget_PerHour_From");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_HoursPerProject",
                schema: "jobs",
                table: "Jobs",
                newName: "Freelance_HoursPerProject");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_HourlyBudget_To",
                schema: "jobs",
                table: "Jobs",
                newName: "Freelance_SalaryBudget_PerMonth_To");

            migrationBuilder.RenameColumn(
                name: "Terms_Freelance_HourlyBudget_From",
                schema: "jobs",
                table: "Jobs",
                newName: "Freelance_SalaryBudget_PerMonth_From");

            migrationBuilder.RenameColumn(
                name: "Terms_EndDate",
                schema: "jobs",
                table: "Jobs",
                newName: "Freelance_EndDate");

            migrationBuilder.RenameColumn(
                name: "Terms_Currency",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_SalaryBudget_Currency");

            migrationBuilder.RenameColumn(
                name: "Terms_PartTimeWorkingHours_Weekly",
                schema: "jobs",
                table: "Jobs",
                newName: "Permanent_MonthlyHours");

            migrationBuilder.AddColumn<string>(
                name: "Freelance_SalaryBudget_Currency",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Freelance_SalaryBudget_PerHour_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Freelance_SalaryBudget_PerHour_To",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Freelance_StartDate",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobFormats",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FormatType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFormats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobFormats_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobWorkingHours",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkingHoursType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobWorkingHours_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobFormats_JobId",
                schema: "jobs",
                table: "JobFormats",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobWorkingHours_JobId",
                schema: "jobs",
                table: "JobWorkingHours",
                column: "JobId");
        }
    }
}
