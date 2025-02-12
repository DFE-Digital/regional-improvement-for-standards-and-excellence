using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageSchoolImprovement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class auditfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "RISE",
                table: "SupportProject",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "RISE",
                table: "SupportProject",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "RISE",
                table: "SupportProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "RISE",
                table: "SupportProject",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "RISE",
                table: "SupportProject");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "RISE",
                table: "SupportProject");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "RISE",
                table: "SupportProject");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "RISE",
                table: "SupportProject");
        }
    }
}
