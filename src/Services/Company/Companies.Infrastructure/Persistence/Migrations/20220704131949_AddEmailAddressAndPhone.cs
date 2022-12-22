using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    public partial class AddEmailAddressAndPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_CompanyId_Email",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "companies",
                table: "ContactPersons",
                newName: "Phone_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "companies",
                table: "ContactPersons",
                newName: "Email_Address");

            migrationBuilder.RenameColumn(
                name: "Address_Region",
                schema: "companies",
                table: "Companies",
                newName: "Address_PostalCode");

            migrationBuilder.AddColumn<bool>(
                name: "Email_IsVerified",
                schema: "companies",
                table: "ContactPersons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Email_VerificationKey",
                schema: "companies",
                table: "ContactPersons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Email_VerificationRequestedAt",
                schema: "companies",
                table: "ContactPersons",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Email_VerifiedAt",
                schema: "companies",
                table: "ContactPersons",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_CountryCode",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_Number",
                schema: "companies",
                table: "ContactPersons",
                type: "nvarchar(28)",
                maxLength: 28,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Address_Coordinates_Latitude",
                schema: "companies",
                table: "Companies",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Address_Coordinates_Longitude",
                schema: "companies",
                table: "Companies",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Location",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_Email_Address",
                schema: "companies",
                table: "ContactPersons",
                column: "Email_Address",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_Email_Address",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Email_IsVerified",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Email_VerificationKey",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Email_VerificationRequestedAt",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Email_VerifiedAt",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Phone_CountryCode",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Phone_Number",
                schema: "companies",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "Address_City",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address_Coordinates_Latitude",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address_Coordinates_Longitude",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address_Location",
                schema: "companies",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Phone_PhoneNumber",
                schema: "companies",
                table: "ContactPersons",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email_Address",
                schema: "companies",
                table: "ContactPersons",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                schema: "companies",
                table: "Companies",
                newName: "Address_Region");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_CompanyId_Email",
                schema: "companies",
                table: "ContactPersons",
                columns: new[] { "CompanyId", "Email" },
                unique: true);
        }
    }
}
