using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddPointToCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Address_Coordinates_Point",
                schema: "candidate",
                table: "Candidates",
                type: "geography",
                nullable: true);

            var sqlScript = SqlScriptsProvider.Get("20220905132040").GetAwaiter().GetResult();
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Coordinates_Point",
                schema: "candidate",
                table: "Candidates");
        }
    }
}
