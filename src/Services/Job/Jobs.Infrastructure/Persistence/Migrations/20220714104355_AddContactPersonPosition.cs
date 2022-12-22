using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class AddContactPersonPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position_Code",
                schema: "jobs",
                table: "JobContactPersons",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Position_Id",
                schema: "jobs",
                table: "JobContactPersons",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position_Code",
                schema: "jobs",
                table: "JobContactPersons");

            migrationBuilder.DropColumn(
                name: "Position_Id",
                schema: "jobs",
                table: "JobContactPersons");
        }
    }
}
