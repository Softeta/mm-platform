using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class CreateImageValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUri",
                schema: "candidate",
                table: "Candidates",
                newName: "Picture_ThumbnailUri");

            migrationBuilder.AddColumn<string>(
                name: "Picture_OriginalUri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture_OriginalUri",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Picture_ThumbnailUri",
                schema: "candidate",
                table: "Candidates",
                newName: "PictureUri");
        }
    }
}
