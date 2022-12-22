﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    public partial class ExtendCandidateLanguageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "candidate",
                table: "CandidateLanguages",
                newName: "Language_Code");

            migrationBuilder.AddColumn<Guid>(
                name: "Language_Id",
                schema: "candidate",
                table: "CandidateLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Language_Name",
                schema: "candidate",
                table: "CandidateLanguages",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language_Id",
                schema: "candidate",
                table: "CandidateLanguages");

            migrationBuilder.DropColumn(
                name: "Language_Name",
                schema: "candidate",
                table: "CandidateLanguages");

            migrationBuilder.RenameColumn(
                name: "Language_Code",
                schema: "candidate",
                table: "CandidateLanguages",
                newName: "Code");
        }
    }
}
