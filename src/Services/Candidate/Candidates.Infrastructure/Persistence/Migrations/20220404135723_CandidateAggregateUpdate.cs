using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class CandidateAggregateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "AvailableFrom",
                schema: "candidate",
                table: "Candidates",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "Address_FullAddress",
                schema: "candidate",
                table: "Candidates",
                newName: "DesiredPosition");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndDate",
                schema: "candidate",
                table: "CandidateWorkTypes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumSalary_Currency",
                schema: "candidate",
                table: "CandidateWorkTypes",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSalary_PerHour",
                schema: "candidate",
                table: "CandidateWorkTypes",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSalary_PerMonth",
                schema: "candidate",
                table: "CandidateWorkTypes",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "candidate",
                table: "CandidateWorkTypes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkHours",
                schema: "candidate",
                table: "CandidateWorkTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingHoursType",
                schema: "candidate",
                table: "CandidateWorkTypes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_AddressLine",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Address_Coordinates_Latitude",
                schema: "candidate",
                table: "Candidates",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Address_Coordinates_Longitude",
                schema: "candidate",
                table: "Candidates",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesiredGoals",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormatType",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hobbies",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkHours",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CandidateLanguages",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateLanguages_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateWorkLocations",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateWorkLocations_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateLanguages_CandidateId",
                schema: "candidate",
                table: "CandidateLanguages",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkLocations_CandidateId",
                schema: "candidate",
                table: "CandidateWorkLocations",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateLanguages",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateWorkLocations",
                schema: "candidate");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "candidate",
                table: "CandidateWorkTypes");

            migrationBuilder.DropColumn(
                name: "MinimumSalary_Currency",
                schema: "candidate",
                table: "CandidateWorkTypes");

            migrationBuilder.DropColumn(
                name: "MinimumSalary_PerHour",
                schema: "candidate",
                table: "CandidateWorkTypes");

            migrationBuilder.DropColumn(
                name: "MinimumSalary_PerMonth",
                schema: "candidate",
                table: "CandidateWorkTypes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "candidate",
                table: "CandidateWorkTypes");

            migrationBuilder.DropColumn(
                name: "WorkHours",
                schema: "candidate",
                table: "CandidateWorkTypes");

            migrationBuilder.DropColumn(
                name: "WorkingHoursType",
                schema: "candidate",
                table: "CandidateWorkTypes");

            migrationBuilder.DropColumn(
                name: "Address_AddressLine",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Address_Coordinates_Latitude",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Address_Coordinates_Longitude",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "DesiredGoals",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "FormatType",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Hobbies",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "WorkHours",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "DesiredPosition",
                schema: "candidate",
                table: "Candidates",
                newName: "Address_FullAddress");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                schema: "candidate",
                table: "Candidates",
                newName: "AvailableFrom");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);
        }
    }
}
