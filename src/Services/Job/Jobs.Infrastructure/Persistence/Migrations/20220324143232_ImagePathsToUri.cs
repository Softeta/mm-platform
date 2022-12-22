using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ImagePathsToUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Company_LogoPath",
                schema: "jobs",
                table: "Jobs",
                newName: "Company_LogoUri");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                schema: "jobs",
                table: "JobContactPersons",
                newName: "PictureUri");

            migrationBuilder.AlterColumn<string>(
                name: "Company_LogoUri",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PictureUri",
                schema: "jobs",
                table: "JobContactPersons",
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
               name: "Company_LogoUri",
               schema: "jobs",
               table: "Jobs",
               newName: "Company_LogoPath");

            migrationBuilder.RenameColumn(
                name: "PictureUri",
                schema: "jobs",
                table: "JobContactPersons",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "Company_LogoPath",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                schema: "jobs",
                table: "JobContactPersons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
