using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addlatosupportproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalAuthority",
                schema: "RISE",
                table: "SupportProject",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalAuthority",
                schema: "RISE",
                table: "SupportProject");
        }
    }
}
