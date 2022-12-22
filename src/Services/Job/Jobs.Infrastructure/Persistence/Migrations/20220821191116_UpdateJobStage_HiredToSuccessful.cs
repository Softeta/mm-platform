using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class UpdateJobStage_HiredToSuccessful : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = SqlScriptsProvider.Get("20220821191116").GetAwaiter().GetResult();
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
