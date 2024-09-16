using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoping_cart.Migrations
{
    /// <inheritdoc />
    public partial class cartmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Cart_id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    OrderDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Product_TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "NNXZOJ+ZZ9vcltL7+nXTMQ==.O3G0LJIDJ2I671lKsnH/Jk7UeGUbp6VuMQh/NE3X7dY=");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Product_id",
                table: "Carts",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_User_id",
                table: "Carts",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Product_id",
                table: "Orders",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_User_id",
                table: "Orders",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "pJE68PgqqG1K7/IARaKsaw==.hFL8MnwYC4a9WX6tLVth9asWziTJyna/RXH6Sp3vblg=");
        }
    }
}
