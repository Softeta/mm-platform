using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class RemoveShortlistSendDateFromSelectedCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortListSendDate",
                schema: "jobs",
                table: "SelectedCandidate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ShortListSendDate",
                schema: "jobs",
                table: "SelectedCandidate",
                type: "datetimeoffset",
                nullable: true);
        }
    }
}
