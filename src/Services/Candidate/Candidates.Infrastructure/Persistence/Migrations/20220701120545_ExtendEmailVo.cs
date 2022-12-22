using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ExtendEmailVo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Email_VerificationKey",
                schema: "candidate",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Email_VerificationRequestedAt",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email_VerificationKey",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Email_VerificationRequestedAt",
                schema: "candidate",
                table: "Candidates");
        }
    }
}
