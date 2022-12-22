using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class AddWebsitesAndIndustries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GlassdoorUrl",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyIndustries",
                schema: "companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndustryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyIndustries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyIndustries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyIndustries_CompanyId",
                schema: "companies",
                table: "CompanyIndustries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyIndustries_CompanyId_IndustryId",
                schema: "companies",
                table: "CompanyIndustries",
                columns: new[] { "CompanyId", "IndustryId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyIndustries",
                schema: "companies");

            migrationBuilder.DropColumn(
                name: "GlassdoorUrl",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                schema: "companies",
                table: "Companies");
        }
    }
}
