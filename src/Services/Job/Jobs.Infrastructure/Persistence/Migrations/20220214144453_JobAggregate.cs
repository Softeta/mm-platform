using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class JobAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Company_Id",
                schema: "jobs",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Company_Name",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeadlineDate",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(3500)",
                maxLength: 3500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeeRange_Currency",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerHour_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerHour_To",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerMonth_From",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeRange_PerMonth_To",
                schema: "jobs",
                table: "Jobs",
                type: "Decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovalNeeded",
                schema: "jobs",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Owner_FirstName",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Owner_Id",
                schema: "jobs",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Owner_LastName",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "jobs",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkHours",
                schema: "jobs",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearExperience_From",
                schema: "jobs",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearExperience_To",
                schema: "jobs",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssignedEmployees",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Employee_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Employee_FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Employee_LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedEmployees_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobFormats",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormatType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFormats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobFormats_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobLanguages",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobLanguages_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRegions",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRegions_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeniorities",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Seniority = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeniorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeniorities_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSkills_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobWorkTypes",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorkTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobWorkTypes_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedEmployees_JobId",
                schema: "jobs",
                table: "AssignedEmployees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobFormats_JobId",
                schema: "jobs",
                table: "JobFormats",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLanguages_JobId",
                schema: "jobs",
                table: "JobLanguages",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRegions_JobId",
                schema: "jobs",
                table: "JobRegions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeniorities_JobId",
                schema: "jobs",
                table: "JobSeniorities",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId",
                schema: "jobs",
                table: "JobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobWorkTypes_JobId",
                schema: "jobs",
                table: "JobWorkTypes",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedEmployees",
                schema: "jobs");

            migrationBuilder.DropTable(
                name: "JobFormats",
                schema: "jobs");

            migrationBuilder.DropTable(
                name: "JobLanguages",
                schema: "jobs");

            migrationBuilder.DropTable(
                name: "JobRegions",
                schema: "jobs");

            migrationBuilder.DropTable(
                name: "JobSeniorities",
                schema: "jobs");

            migrationBuilder.DropTable(
                name: "JobSkills",
                schema: "jobs");

            migrationBuilder.DropTable(
                name: "JobWorkTypes",
                schema: "jobs");

            migrationBuilder.DropColumn(
                name: "Company_Id",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Company_Name",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ContractType",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_Currency",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerHour_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerHour_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerMonth_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FeeRange_PerMonth_To",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsApprovalNeeded",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Owner_FirstName",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Owner_Id",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Owner_LastName",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Position",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkHours",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "YearExperience_From",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "YearExperience_To",
                schema: "jobs",
                table: "Jobs");
        }
    }
}
