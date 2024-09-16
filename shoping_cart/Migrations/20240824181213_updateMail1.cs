using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoping_cart.Migrations
{
    /// <inheritdoc />
    public partial class updateMail1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "A24CGUZ+JnENPKx63a1ADA==.cA38pUud6XeT8zgnugjbBHxHKldHhQALVePfRZsxNyY=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "Uz+tNSyJe0QQUWOY2BF7tA==.kslAYCBJNLDtBUQsF2Xcw2zbzJqVT7zldeC6D5AhLq8=");
        }
    }
}
