using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class ImagePathsToUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoPath",
                schema: "companies",
                table: "Companies",
                newName: "LogoUri");

            migrationBuilder.RenameColumn(
                name: "LogoPath",
                schema: "companies",
                table: "ContactPersons",
                newName: "PictureUri");

            migrationBuilder.AlterColumn<string>(
                name: "LogoUri",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PictureUri",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUri",
                schema: "companies",
                table: "Companies",
                newName: "LogoPath");

            migrationBuilder.RenameColumn(
                name: "PictureUri",
                schema: "companies",
                table: "ContactPersons",
                newName: "LogoPath");

            migrationBuilder.AlterColumn<string>(
                name: "LogoPath",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "LogoPath",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
