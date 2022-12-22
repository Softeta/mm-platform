using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddExternalIdOnCandidateTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateTests",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "CandidateOldPlatformId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                schema: "candidate",
                table: "CandidateTests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateTests",
                schema: "candidate",
                table: "CandidateTests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTests_ExternalId",
                schema: "candidate",
                table: "CandidateTests",
                column: "ExternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateTests",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropIndex(
                name: "IX_CandidateTests_ExternalId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.AddColumn<long>(
                name: "CandidateOldPlatformId",
                schema: "candidate",
                table: "CandidateTests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateTests",
                schema: "candidate",
                table: "CandidateTests",
                column: "CandidateOldPlatformId");
        }
    }
}
