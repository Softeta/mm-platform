using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class HobbyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hobbies",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.CreateTable(
                name: "CandidateHobbies",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    HobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateHobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateHobbies_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateHobbies_CandidateId",
                schema: "candidate",
                table: "CandidateHobbies",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateHobbies_CandidateId_HobbyId",
                schema: "candidate",
                table: "CandidateHobbies",
                columns: new[] { "CandidateId", "HobbyId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateHobbies",
                schema: "candidate");

            migrationBuilder.AddColumn<string>(
                name: "Hobbies",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }
    }
}
