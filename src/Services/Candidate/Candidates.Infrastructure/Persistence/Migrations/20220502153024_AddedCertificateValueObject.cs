using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddedCertificateValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CertificateUri",
                schema: "candidate",
                table: "CandidateEducations",
                newName: "Certificate_Uri");

            migrationBuilder.RenameColumn(
                name: "CertificateUri",
                schema: "candidate",
                table: "CandidateCourses",
                newName: "Certificate_Uri");

            migrationBuilder.AddColumn<string>(
                name: "Certificate_FileName",
                schema: "candidate",
                table: "CandidateEducations",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certificate_FileName",
                schema: "candidate",
                table: "CandidateCourses",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certificate_FileName",
                schema: "candidate",
                table: "CandidateEducations");

            migrationBuilder.DropColumn(
                name: "Certificate_FileName",
                schema: "candidate",
                table: "CandidateCourses");

            migrationBuilder.RenameColumn(
                name: "Certificate_Uri",
                schema: "candidate",
                table: "CandidateEducations",
                newName: "CertificateUri");

            migrationBuilder.RenameColumn(
                name: "Certificate_Uri",
                schema: "candidate",
                table: "CandidateCourses",
                newName: "CertificateUri");
        }
    }
}
