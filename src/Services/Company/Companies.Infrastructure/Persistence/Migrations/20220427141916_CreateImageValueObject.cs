using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class CreateImageValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUri",
                schema: "companies",
                table: "ContactPersons",
                newName: "Picture_ThumbnailUri");

            migrationBuilder.RenameColumn(
                name: "LogoUri",
                schema: "companies",
                table: "Companies",
                newName: "Logo_ThumbnailUri");

            migrationBuilder.AddColumn<string>(
                name: "Picture_OriginalUri",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo_OriginalUri",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture_OriginalUri",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Logo_OriginalUri",
                schema: "companies",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Picture_ThumbnailUri",
                schema: "companies",
                table: "ContactPersons",
                newName: "PictureUri");

            migrationBuilder.RenameColumn(
                name: "Logo_ThumbnailUri",
                schema: "companies",
                table: "Companies",
                newName: "LogoUri");
        }
    }
}
