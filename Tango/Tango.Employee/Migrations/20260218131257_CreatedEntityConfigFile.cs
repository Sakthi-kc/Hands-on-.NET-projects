using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tango.Employee.Migrations
{
    /// <inheritdoc />
    public partial class CreatedEntityConfigFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Employees",
                newName: "DepartmentName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentName",
                table: "Employees",
                newName: "Department");
        }
    }
}
