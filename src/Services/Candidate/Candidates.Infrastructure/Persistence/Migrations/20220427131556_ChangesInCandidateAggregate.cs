using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ChangesInCandidateAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesiredGoals",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "DesiredPosition",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.AddColumn<string>(
                name: "PersonalWebsiteUrl",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "CandidateIndustries",
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
                    table.PrimaryKey("PK_CandidateIndustries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateIndustries_Candidates_CandidateId",
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

            migrationBuilder.CreateIndex(
                name: "IX_CandidateIndustries_CandidateId",
                schema: "candidate",
                table: "CandidateIndustries",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateDesiredSkills",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateIndustries",
                schema: "candidate");

            migrationBuilder.DropColumn(
                name: "PersonalWebsiteUrl",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.AddColumn<string>(
                name: "DesiredGoals",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesiredPosition",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
