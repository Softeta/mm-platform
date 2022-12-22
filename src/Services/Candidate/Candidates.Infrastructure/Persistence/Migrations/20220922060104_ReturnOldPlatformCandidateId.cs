using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ReturnOldPlatformCandidateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CandidateOldPlatformId",
                schema: "candidate",
                table: "CandidateTests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTests_CandidateOldPlatformId",
                schema: "candidate",
                table: "CandidateTests",
                column: "CandidateOldPlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CandidateTests_CandidateOldPlatformId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "CandidateOldPlatformId",
                schema: "candidate",
                table: "CandidateTests");
        }
    }
}
