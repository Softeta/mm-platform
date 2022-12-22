using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddCandidatesApliedInJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateAppliedInJobs",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobStage = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Position_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position_Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Position_AliasTo_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Position_AliasTo_Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company_Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Company_LogoUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Freelance = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Permanent = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeadlineDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsJobArchived = table.Column<bool>(type: "bit", nullable: false),
                    InvitedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateAppliedInJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateAppliedInJobs_CandidateJobs_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "CandidateJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAppliedInJobs_CandidateId",
                schema: "candidate",
                table: "CandidateAppliedInJobs",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAppliedInJobs_CandidateId_JobId",
                schema: "candidate",
                table: "CandidateAppliedInJobs",
                columns: new[] { "CandidateId", "JobId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateAppliedInJobs",
                schema: "candidate");
        }
    }
}
