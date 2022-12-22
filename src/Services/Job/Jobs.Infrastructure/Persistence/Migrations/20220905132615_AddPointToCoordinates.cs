using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class AddPointToCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Company_Address_Coordinates_Point",
                schema: "jobs",
                table: "Jobs",
                type: "geography",
                nullable: true);

            var sqlScript = SqlScriptsProvider.Get("20220905132615").GetAwaiter().GetResult();
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_Address_Coordinates_Point",
                schema: "jobs",
                table: "Jobs");
        }
    }
}
