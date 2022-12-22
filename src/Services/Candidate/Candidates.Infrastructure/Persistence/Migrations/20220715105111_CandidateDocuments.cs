using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class CandidateDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Picture_ThumbnailUri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Picture_OriginalUri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio_FileName",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio_Uri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurriculumVitae_FileName",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurriculumVitae_Uri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video_FileName",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video_Uri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Certificate_Uri",
                schema: "candidate",
                table: "CandidateEducations",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Certificate_Uri",
                schema: "candidate",
                table: "CandidateCourses",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio_FileName",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Bio_Uri",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CurriculumVitae_FileName",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CurriculumVitae_Uri",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Video_FileName",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Video_Uri",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "Picture_ThumbnailUri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Picture_OriginalUri",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Certificate_Uri",
                schema: "candidate",
                table: "CandidateEducations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Certificate_Uri",
                schema: "candidate",
                table: "CandidateCourses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);
        }
    }
}
