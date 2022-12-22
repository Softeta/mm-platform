using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class CreateJobCandidates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedCandidate_Jobs_JobId",
                schema: "jobs",
                table: "ArchivedCandidate");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectedCandidate_Jobs_JobId",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "ShortListSendDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.CreateTable(
                name: "JobCandidates",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Position_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position_Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Company_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company_Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Company_LogoUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShortListSendDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidates", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedCandidate_JobCandidates_JobId",
                schema: "jobs",
                table: "ArchivedCandidate",
                column: "JobId",
                principalSchema: "jobs",
                principalTable: "JobCandidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedCandidate_JobCandidates_JobId",
                schema: "jobs",
                table: "SelectedCandidate",
                column: "JobId",
                principalSchema: "jobs",
                principalTable: "JobCandidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedCandidate_JobCandidates_JobId",
                schema: "jobs",
                table: "ArchivedCandidate");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectedCandidate_JobCandidates_JobId",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropTable(
                name: "JobCandidates",
                schema: "jobs");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ShortListSendDate",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedCandidate_Jobs_JobId",
                schema: "jobs",
                table: "ArchivedCandidate",
                column: "JobId",
                principalSchema: "jobs",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedCandidate_Jobs_JobId",
                schema: "jobs",
                table: "SelectedCandidate",
                column: "JobId",
                principalSchema: "jobs",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
