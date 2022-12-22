using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class UpdateCandidateAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerHour_From",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerHour_To",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Location_Country",
                schema: "candidate",
                table: "Candidates",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "FeeRange_Currency",
                schema: "candidate",
                table: "Candidates",
                newName: "MinimumSalary_Currency");

            migrationBuilder.RenameColumn(
                name: "FeeRange_PerMonth_To",
                schema: "candidate",
                table: "Candidates",
                newName: "MinimumSalary_PerMonth");

            migrationBuilder.RenameColumn(
                name: "FeeRange_PerMonth_From",
                schema: "candidate",
                table: "Candidates",
                newName: "MinimumSalary_PerHour");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationPreference_PhoneNumber",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_FullAddress",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OpenForOpportunities",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PictureUri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CandidateContractTypes",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateContractTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateContractTypes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateContractTypes_CandidateId",
                schema: "candidate",
                table: "CandidateContractTypes",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateContractTypes",
                schema: "candidate");

            migrationBuilder.DropColumn(
                name: "Address_City",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Address_FullAddress",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "OpenForOpportunities",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "PictureUri",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "MinimumSalary_Currency",
                schema: "candidate",
                table: "Candidates",
                newName: "FeeRange_Currency");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                schema: "candidate",
                table: "Candidates",
                newName: "Location_Country");

            migrationBuilder.RenameColumn(
                name: "MinimumSalary_PerMonth",
                schema: "candidate",
                table: "Candidates",
                newName: "FeeRange_PerMonth_To");

            migrationBuilder.RenameColumn(
                name: "MinimumSalary_PerHour",
                schema: "candidate",
                table: "Candidates",
                newName: "FeeRange_PerMonth_From");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationPreference_PhoneNumber",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FeeRange_Currency",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location_Country",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerHour_From",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerHour_To",
                schema: "candidate",
                table: "Candidates",
                type: "Decimal(18,2)",
                nullable: true);
        }
    }
}
