using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class PictureUriToOwnerAndAssignedTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner_PictureUri",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Employee_PictureUri",
                schema: "jobs",
                table: "AssignedEmployees",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner_PictureUri",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Employee_PictureUri",
                schema: "jobs",
                table: "AssignedEmployees");
        }
    }
}
