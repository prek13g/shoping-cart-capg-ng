using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoping_cart.Migrations
{
    /// <inheritdoc />
    public partial class changeAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                columns: new[] { "AdminEmail", "AdminName", "AdminPassword" },
                values: new object[] { "Anu@gmail.com", "Anu", "4zb3OaavacylqBnYXVVAqA==.7cFf+ImWeCXoHq2NYDstzgeecX6EqSjV2cJSsiCg/K0=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                columns: new[] { "AdminEmail", "AdminName", "AdminPassword" },
                values: new object[] { "Ashok@gmail.com", "Ashok", "NNXZOJ+ZZ9vcltL7+nXTMQ==.O3G0LJIDJ2I671lKsnH/Jk7UeGUbp6VuMQh/NE3X7dY=" });
        }
    }
}
