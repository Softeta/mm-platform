using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ExtendCandidateNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                schema: "candidate",
                table: "Candidates",
                newName: "Note_Value");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Note_EndDate",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note_EndDate",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Note_Value",
                schema: "candidate",
                table: "Candidates",
                newName: "Note");
        }
    }
}
