using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ExtendCandidateAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateCourses",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IssuingOrganization = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CertificateUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateCourses_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateEducations",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    To = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StudiesDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CertificateUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateEducations_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateWorkExperiences",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    To = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    JobDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateWorkExperiences_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "candidate",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateWorkExperienceSkills",
                schema: "candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateWorkExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkExperienceSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateWorkExperienceSkills_CandidateWorkExperiences_CandidateWorkExperienceId",
                        column: x => x.CandidateWorkExperienceId,
                        principalSchema: "candidate",
                        principalTable: "CandidateWorkExperiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCourses_CandidateId",
                schema: "candidate",
                table: "CandidateCourses",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducations_CandidateId",
                schema: "candidate",
                table: "CandidateEducations",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkExperiences_CandidateId",
                schema: "candidate",
                table: "CandidateWorkExperiences",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkExperienceSkills_CandidateWorkExperienceId",
                schema: "candidate",
                table: "CandidateWorkExperienceSkills",
                column: "CandidateWorkExperienceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateCourses",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateEducations",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateWorkExperienceSkills",
                schema: "candidate");

            migrationBuilder.DropTable(
                name: "CandidateWorkExperiences",
                schema: "candidate");
        }
    }
}
