using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddedCurrentJobFlagToWorkExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateDesiredSkills",
                schema: "candidate");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentJob",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId1",
                schema: "candidate",
                table: "CandidateSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills",
                column: "CandidateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkills_Candidates_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills",
                column: "CandidateId1",
                principalSchema: "candidate",
                principalTable: "Candidates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkills_Candidates_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkills_CandidateId1",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.DropColumn(
                name: "IsCurrentJob",
                schema: "candidate",
                table: "CandidateWorkExperiences");

            migrationBuilder.DropColumn(
                name: "CandidateId1",
                schema: "candidate",
                table: "CandidateSkills");

            migrationBuilder.CreateTable(
                name: "CandidateDesiredSkills",
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
                    table.PrimaryKey("PK_CandidateDesiredSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateDesiredSkills_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDesiredSkills_CandidateId",
                schema: "candidate",
                table: "CandidateDesiredSkills",
                column: "CandidateId");
        }
    }
}
