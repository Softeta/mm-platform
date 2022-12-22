using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class UpdateJobStage_HiredToSuccessful : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = SqlScriptsProvider.Get("20220821191410").GetAwaiter().GetResult();
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
