using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    public partial class ExtendJobAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_Region",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Company_AddressLine",
                schema: "jobs",
                table: "Jobs",
                newName: "Company_Address_AddressLine");

            migrationBuilder.AddColumn<double>(
                name: "Company_Address_Coordinates_Latitude",
                schema: "jobs",
                table: "Jobs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Company_Address_Coordinates_Longitude",
                schema: "jobs",
                table: "Jobs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainContact",
                schema: "jobs",
                table: "JobContactPersons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                schema: "jobs",
                table: "JobContactPersons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_Address_Coordinates_Latitude",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Company_Address_Coordinates_Longitude",
                schema: "jobs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsMainContact",
                schema: "jobs",
                table: "JobContactPersons");

            migrationBuilder.DropColumn(
                name: "PersonId",
                schema: "jobs",
                table: "JobContactPersons");

            migrationBuilder.RenameColumn(
                name: "Company_Address_AddressLine",
                schema: "jobs",
                table: "Jobs",
                newName: "Company_AddressLine");

            migrationBuilder.AddColumn<string>(
                name: "Company_Region",
                schema: "jobs",
                table: "Jobs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }
    }
}
