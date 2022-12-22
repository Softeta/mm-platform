using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddCandidateTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateTests",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestsPackages",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TestsUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestsPackages_CandidateTests_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "CandidateTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestInstances",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageTechnicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TestTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CompleteDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Scores = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestInstances_TestsPackages_PackageTechnicalId",
                        column: x => x.PackageTechnicalId,
                        principalSchema: "candidate",
                        principalTable: "TestsPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestReports",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageTechnicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestReports_TestsPackages_PackageTechnicalId",
                        column: x => x.PackageTechnicalId,
                        principalSchema: "candidate",
                        principalTable: "TestsPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestInstances_PackageTechnicalId",
                schema: "candidate",
                table: "TestInstances",
                column: "PackageTechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInstances_TestTypeId",
                schema: "candidate",
                table: "TestInstances",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInstances_TestTypeId_PackageId",
                schema: "candidate",
                table: "TestInstances",
                columns: new[] { "TestTypeId", "PackageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestReports_PackageTechnicalId",
                schema: "candidate",
                table: "TestReports",
                column: "PackageTechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_TestReports_ReportId",
                schema: "candidate",
                table: "TestReports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TestReports_ReportId_PackageId",
                schema: "candidate",
                table: "TestReports",
                columns: new[] { "ReportId", "PackageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestsPackages_CandidateId",
                schema: "candidate",
                table: "TestsPackages",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_TestsPackages_CandidateId_PackageId",
                schema: "candidate",
                table: "TestsPackages",
                columns: new[] { "CandidateId", "PackageId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestInstances",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "TestReports",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "TestsPackages",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateTests",
                schema: "candidate");
        }
    }
}
