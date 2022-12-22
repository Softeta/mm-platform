using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class AddRegistrationFieldOnContactPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<bool>(
                name: "MarketingAcknowledgement_Agreed",
                schema: "companies",
                table: "ContactPersons",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "MarketingAcknowledgement_ModifiedAt",
                schema: "companies",
                table: "ContactPersons",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stage",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemLanguage",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TermsAndConditions_Agreed",
                schema: "companies",
                table: "ContactPersons",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TermsAndConditions_ModifiedAt",
                schema: "companies",
                table: "ContactPersons",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketingAcknowledgement_Agreed",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "MarketingAcknowledgement_ModifiedAt",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Stage",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "SystemLanguage",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "TermsAndConditions_Agreed",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "TermsAndConditions_ModifiedAt",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);
        }
    }
}
