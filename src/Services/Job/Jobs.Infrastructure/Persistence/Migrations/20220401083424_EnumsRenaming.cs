using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class EnumsRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobWorkTypes",
                schema: "jobs");

            migrationBuilder.RenameColumn(
                name: "ContractType",
                schema: "jobs",
                table: "Jobs",
                newName: "WorkType");

            migrationBuilder.CreateTable(
                name: "JobWorkingHours",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkingHoursType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
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
                name: "IX_JobWorkingHours_JobId",
                schema: "jobs",
                table: "JobWorkingHours",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobWorkingHours",
                schema: "jobs");

            migrationBuilder.RenameColumn(
                name: "WorkType",
                schema: "jobs",
                table: "Jobs",
                newName: "ContractType");

            migrationBuilder.CreateTable(
                name: "JobWorkTypes",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorkTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobWorkTypes_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobWorkTypes_JobId",
                schema: "jobs",
                table: "JobWorkTypes",
                column: "JobId");
        }
    }
}
