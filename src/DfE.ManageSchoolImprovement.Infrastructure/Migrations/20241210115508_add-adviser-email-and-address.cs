using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DfE.ManageSchoolImprovement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addadviseremailandaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedUser",
                schema: "RISE",
                table: "SupportProject",
                newName: "AssignedAdviserFullName");

            migrationBuilder.AddColumn<string>(
                name: "AssignedAdviserEmailAddress",
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
                name: "AssignedAdviserEmailAddress",
                schema: "RISE",
                table: "SupportProject");

            migrationBuilder.RenameColumn(
                name: "AssignedAdviserFullName",
                schema: "RISE",
                table: "SupportProject",
                newName: "AssignedUser");
        }
    }
}
