using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class AddSearchIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchIndexes",
                schema: "companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Index = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchIndexes_RegistryCenterCompanies_RegistrationNumber_CountryCode",
                        columns: x => new { x.RegistrationNumber, x.CountryCode },
                        principalSchema: "companies",
                        principalTable: "RegistryCenterCompanies",
                        principalColumns: new[] { "RegistrationNumber", "Country" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistryCenterCompanies_RegistrationNumber",
                schema: "companies",
                table: "RegistryCenterCompanies",
                column: "RegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_SearchIndexes_Index",
                schema: "companies",
                table: "SearchIndexes",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_SearchIndexes_RegistrationNumber_CountryCode",
                schema: "companies",
                table: "SearchIndexes",
                columns: new[] { "RegistrationNumber", "CountryCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchIndexes",
                schema: "companies");

            migrationBuilder.DropIndex(
                name: "IX_RegistryCenterCompanies_RegistrationNumber",
                schema: "companies",
                table: "RegistryCenterCompanies");
        }
    }
}
