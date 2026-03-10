using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Management.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldTypeToBit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE When StockQuantity > 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END",
                stored: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "CASE When StockQuantity > 0 THEN 1 ELSE 0 END",
                oldStored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE When StockQuantity > 0 THEN 1 ELSE 0 END",
                stored: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "CASE When StockQuantity > 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END",
                oldStored: true);
        }
    }
}
