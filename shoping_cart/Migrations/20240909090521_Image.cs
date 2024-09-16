using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoping_cart.Migrations
{
    /// <inheritdoc />
    public partial class Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "64EH2civr6sChvBN0Yo5bQ==.qValNUwkiqe4YfGYerB897fTJbaiTMM8HX81MK33twg=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "A24CGUZ+JnENPKx63a1ADA==.cA38pUud6XeT8zgnugjbBHxHKldHhQALVePfRZsxNyY=");
        }
    }
}
