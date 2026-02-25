using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Product_Management.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductEntityModel",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, computedColumnSql: "CASE When StockQuantity > 0 THEN 1 ELSE 0 END", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntityModel", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "ProductEntityModel",
                columns: new[] { "ProductId", "Category", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Electronics", 599.5m, "Wireless mouse", 10 },
                    { 2, "Electronics", 899.0m, "USB Charger", 7 },
                    { 3, "Wearable", 999m, "SmartWatch", 2 },
                    { 4, "Home & Office", 1499.25m, "Work table", 30 },
                    { 5, "Home & Office", 99m, "Desk lamp", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEntityModel");
        }
    }
}
