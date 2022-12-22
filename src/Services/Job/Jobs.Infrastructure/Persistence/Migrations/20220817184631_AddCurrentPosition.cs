using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class AddCurrentPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position_Code",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_Id",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_Code",
                schema: "jobs",
                table: "ArchivedCandidate",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_Id",
                schema: "jobs",
                table: "ArchivedCandidate",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position_Code",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "Position_Id",
                schema: "jobs",
                table: "SelectedCandidate");

            migrationBuilder.DropColumn(
                name: "Position_Code",
                schema: "jobs",
                table: "ArchivedCandidate");

            migrationBuilder.DropColumn(
                name: "Position_Id",
                schema: "jobs",
                table: "ArchivedCandidate");
        }
    }
}
