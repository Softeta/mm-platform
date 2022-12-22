using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ChangeCandidateTestsStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateTests",
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

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LogicalAssessment_CompletedAt",
                schema: "candidate",
                table: "CandidateTests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogicalAssessment_PackageInstanceId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogicalAssessment_PackageTypeId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LogicalAssessment_Scores_Abstract",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LogicalAssessment_Scores_Accuracy",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LogicalAssessment_Scores_Numerical",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LogicalAssessment_Scores_Speed",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LogicalAssessment_Scores_Total",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LogicalAssessment_Scores_Verbal",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LogicalAssessment_StartedAt",
                schema: "candidate",
                table: "CandidateTests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogicalAssessment_Status",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogicalAssessment_Url",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PersonalityAssessment_CompletedAt",
                schema: "candidate",
                table: "CandidateTests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalityAssessment_PackageInstanceId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalityAssessment_PackageTypeId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_A1",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_A2",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_AQ",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_R1",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_R2",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_S1",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_S2",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_SD",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_W1",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_W2",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_Y1",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalityAssessment_Scores_Y2",
                schema: "candidate",
                table: "CandidateTests",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PersonalityAssessment_StartedAt",
                schema: "candidate",
                table: "CandidateTests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalityAssessment_Status",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalityAssessment_Url",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CandidateOldPlatformId",
                schema: "candidate",
                table: "Candidates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateTests",
                schema: "candidate",
                table: "CandidateTests",
                column: "CandidateOldPlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateTests",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "CandidateOldPlatformId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_CompletedAt",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_PackageInstanceId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_PackageTypeId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Scores_Abstract",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Scores_Accuracy",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Scores_Numerical",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Scores_Speed",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Scores_Total",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Scores_Verbal",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_StartedAt",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Status",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LogicalAssessment_Url",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_CompletedAt",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_PackageInstanceId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_PackageTypeId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_A1",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_A2",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_AQ",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_R1",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_R2",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_S1",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_S2",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_SD",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_W1",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_W2",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_Y1",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Scores_Y2",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_StartedAt",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Status",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PersonalityAssessment_Url",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "CandidateOldPlatformId",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateTests",
                schema: "candidate",
                table: "CandidateTests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TestsPackages",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TestsUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
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
                    CompleteDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageTechnicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Scores = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TestTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageTechnicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
