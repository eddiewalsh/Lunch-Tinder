using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class StatusAddedtoEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$l0ElG/erYpkJGWGPfgG49uRWmCF3v8vYdSPGWMZnuyJ2FI0kJTtDq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$nyJCea0FuDHqO1hoCGlT4.A/2333TpEDAUHgDTkO59Dmv//j9goeG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$/1Z1IGaxnzJBSojSkfAhQeNf4XJIcaAmqSIjVzdfXcSjliNg8dV.a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$lhhYgQXADjq0cyUaNza0.udO.QKuiDeNqQopJniLIqiMSOO57sXyW");
        }
    }
}
