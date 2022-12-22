using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class AddCandidateEmailVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "candidate",
                table: "Candidates",
                newName: "Email_Address");

            migrationBuilder.AddColumn<bool>(
                name: "Email_IsVerified",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Email_VerifiedAt",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MarketingAcknowledgement_Agreed",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "MarketingAcknowledgement_ModifiedAt",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                schema: "candidate",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SystemLanguage",
                schema: "candidate",
                table: "Candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TermsAndConditions_Agreed",
                schema: "candidate",
                table: "Candidates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TermsAndConditions_ModifiedAt",
                schema: "candidate",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email_IsVerified",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Email_VerifiedAt",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MarketingAcknowledgement_Agreed",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MarketingAcknowledgement_ModifiedAt",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Stage",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "SystemLanguage",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "TermsAndConditions_Agreed",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "TermsAndConditions_ModifiedAt",
                schema: "candidate",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Email_Address",
                schema: "candidate",
                table: "Candidates",
                newName: "Email");
        }
    }
}
