using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class addDateRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Terms_StartDate",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Availability_To");

            migrationBuilder.RenameColumn(
                name: "Terms_EndDate",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_Availability_From");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Terms_Availability_To",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_StartDate");

            migrationBuilder.RenameColumn(
                name: "Terms_Availability_From",
                schema: "jobs",
                table: "Jobs",
                newName: "Terms_EndDate");
        }
    }
}
