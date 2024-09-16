using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoping_cart.Migrations
{
    /// <inheritdoc />
    public partial class updateMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "Uz+tNSyJe0QQUWOY2BF7tA==.kslAYCBJNLDtBUQsF2Xcw2zbzJqVT7zldeC6D5AhLq8=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "4zb3OaavacylqBnYXVVAqA==.7cFf+ImWeCXoHq2NYDstzgeecX6EqSjV2cJSsiCg/K0=");
        }
    }
}
