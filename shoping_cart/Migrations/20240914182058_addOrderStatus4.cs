using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoping_cart.Migrations
{
    /// <inheritdoc />
    public partial class addOrderStatus4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "pKJLTGSzb278EIXLWda1mw==.LxcKWA0hFkDUuUw3JOz0zt5voIQDLTcZ98P2EDd7Ds0=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 123,
                column: "AdminPassword",
                value: "9VYChu9uM4hDFHNlox2wvQ==.g9ITauswxa0SzjAS9dRcXBQmgs8ue74rXB8paRG4hvg=");
        }
    }
}
