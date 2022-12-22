using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ChangeSeniorityLevelNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = SqlScriptsProvider.Get("20220902133156").GetAwaiter().GetResult();
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
