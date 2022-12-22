using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ChangeIsSelectionActivatedToSelectionStarted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSelectionActivated",
                schema: "jobs",
                table: "Jobs",
                newName: "IsSelectionStarted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSelectionStarted",
                schema: "jobs",
                table: "Jobs",
                newName: "IsSelectionActivated");
        }
    }
}
