using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class AddContactPersonCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy_Id",
                schema: "companies",
                table: "ContactPersons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_Scope",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy_Id",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "CreatedBy_Scope",
                schema: "companies",
                table: "ContactPersons");
        }
    }
}
