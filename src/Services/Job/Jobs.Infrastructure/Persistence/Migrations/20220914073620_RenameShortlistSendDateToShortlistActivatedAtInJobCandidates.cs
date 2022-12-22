using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class RenameShortlistSendDateToShortlistActivatedAtInJobCandidates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortListSendDate",
                schema: "jobs",
                table: "JobCandidates",
                newName: "ShortListActivatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortListActivatedAt",
                schema: "jobs",
                table: "JobCandidates",
                newName: "ShortListSendDate");
        }
    }
}
