using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ExtendJobForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovalNeeded",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "Permanent_WorkType",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Permanent_MonthlyHours",
                schema: "jobs",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Permanent_StartDate",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permanent_SalaryBudget_Currency",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Permanent_SalaryBudget_PerHour_To",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Permanent_SalaryBudget_PerHour_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Permanent_SalaryBudget_PerMonth_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Permanent_SalaryBudget_PerMonth_To",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Freelance_EndDate",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Freelance_HoursPerProject",
                schema: "jobs",
                table: "Jobs",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddColumn<decimal>(
                name: "Freelance_SalaryBudget_PerMonth_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Freelance_SalaryBudget_PerMonth_To",
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

            migrationBuilder.AddColumn<string>(
                name: "Freelance_WorkType",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.DropColumn(
                name: "WorkType",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerMonth_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerMonth_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerHour_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerHour_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_Currency",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkHours",
                schema: "jobs",
                table: "Jobs");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Freelance_EndDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_HoursPerProject",
                schema: "jobs",
                table: "Jobs");

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
                name: "Freelance_SalaryBudget_PerMonth_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_SalaryBudget_PerMonth_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_StartDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Freelance_WorkType",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_WorkType",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_StartDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_SalaryBudget_PerMonth_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_SalaryBudget_PerMonth_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_SalaryBudget_PerHour_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_SalaryBudget_PerHour_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_SalaryBudget_Currency",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Permanent_MonthlyHours",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovalNeeded",
                schema: "jobs",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FeeRange_Currency",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerHour_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerHour_To",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerMonth_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerMonth_To",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkHours",
                schema: "jobs",
                table: "Jobs",
                type: "int",
                nullable: true);

        }
    }
}
