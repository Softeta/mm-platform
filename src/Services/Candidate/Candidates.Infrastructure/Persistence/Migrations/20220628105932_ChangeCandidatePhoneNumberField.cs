using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ChangeCandidatePhoneNumberField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommunicationPreference_PhoneNumber",
                schema: "candidate",
                table: "Candidates",
                newName: "Phone_PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Phone_CountryCode",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_Number",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(28)",
                maxLength: 28,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone_CountryCode",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Phone_Number",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Phone_PhoneNumber",
                schema: "candidate",
                table: "Candidates",
                newName: "CommunicationPreference_PhoneNumber");
        }
    }
}
