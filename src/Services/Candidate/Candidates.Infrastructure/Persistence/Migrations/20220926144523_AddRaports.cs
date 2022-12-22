using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddRaports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LgiGeneralFeedback_InstanceId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LgiGeneralFeedback_Url",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PapiDynamicWheel_InstanceId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PapiDynamicWheel_Url",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PapiGeneralFeedback_InstanceId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PapiGeneralFeedback_Url",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PapiGeneralWheel_InstanceId",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PapiGeneralWheel_Url",
                schema: "candidate",
                table: "CandidateTests",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LgiGeneralFeedback_InstanceId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "LgiGeneralFeedback_Url",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PapiDynamicWheel_InstanceId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PapiDynamicWheel_Url",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PapiGeneralFeedback_InstanceId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PapiGeneralFeedback_Url",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PapiGeneralWheel_InstanceId",
                schema: "candidate",
                table: "CandidateTests");

            migrationBuilder.DropColumn(
                name: "PapiGeneralWheel_Url",
                schema: "candidate",
                table: "CandidateTests");
        }
    }
}
