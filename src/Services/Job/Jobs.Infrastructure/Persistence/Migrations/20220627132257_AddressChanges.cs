using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class AddressChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobRegions",
                schema: "jobs");

            migrationBuilder.AddColumn<string>(
                name: "Company_Address_City",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company_Address_Country",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company_Address_PostalCode",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_Address_City",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Company_Address_Country",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Company_Address_PostalCode",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.CreateTable(
                name: "JobRegions",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRegions_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobRegions_JobId",
                schema: "jobs",
                table: "JobRegions",
                column: "JobId");
        }
    }
}
