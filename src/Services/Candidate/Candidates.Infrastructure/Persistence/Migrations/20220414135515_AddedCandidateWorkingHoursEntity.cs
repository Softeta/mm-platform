using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddedCandidateWorkingHoursEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permanent_WorkingHoursType",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.CreateTable(
                name: "CandidateWorkingHours",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkingHoursType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateWorkingHours_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkingHours_CandidateId",
                schema: "candidate",
                table: "CandidateWorkingHours",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateWorkingHours",
                schema: "candidate");

            migrationBuilder.AddColumn<int>(
                name: "Permanent_WorkingHoursType",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                maxLength: 64,
                nullable: true);
        }
    }
}
