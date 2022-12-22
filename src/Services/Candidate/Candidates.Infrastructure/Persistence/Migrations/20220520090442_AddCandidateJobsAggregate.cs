using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddCandidateJobsAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateJobs",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsShortlisted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateArchivedInJobs",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company_Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateArchivedInJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateArchivedInJobs_CandidateJobs_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "CandidateJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateSelectedInJobs",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company_Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSelectedInJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSelectedInJobs_CandidateJobs_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "CandidateJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateArchivedInJobs_CandidateId",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateArchivedInJobs_CandidateId_JobId",
                schema: "candidate",
                table: "CandidateArchivedInJobs",
                columns: new[] { "CandidateId", "JobId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSelectedInJobs_CandidateId",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSelectedInJobs_CandidateId_JobId",
                schema: "candidate",
                table: "CandidateSelectedInJobs",
                columns: new[] { "CandidateId", "JobId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateArchivedInJobs",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateSelectedInJobs",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateJobs",
                schema: "candidate");
        }
    }
}
