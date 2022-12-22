using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ExtendJobLanguageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "jobs",
                table: "JobLanguages",
                newName: "Language_Code");

            migrationBuilder.AddColumn<Guid>(
                name: "Language_Id",
                schema: "jobs",
                table: "JobLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Language_Name",
                schema: "jobs",
                table: "JobLanguages",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language_Id",
                schema: "jobs",
                table: "JobLanguages");

            migrationBuilder.DropColumn(
                name: "Language_Name",
                schema: "jobs",
                table: "JobLanguages");

            migrationBuilder.RenameColumn(
                name: "Language_Code",
                schema: "jobs",
                table: "JobLanguages",
                newName: "Code");
        }
    }
}
