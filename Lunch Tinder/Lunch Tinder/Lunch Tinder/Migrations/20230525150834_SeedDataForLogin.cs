using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 5, 25, 15, 13, 33, 917, DateTimeKind.Utc).AddTicks(2051), new DateTime(2023, 5, 25, 15, 8, 33, 917, DateTimeKind.Utc).AddTicks(2049) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "LunchGroupsGroupId", "Password", "UserName", "UserType" },
                values: new object[,]
                {
                    { 1, "useremail@gmail.com", null, "$2a$11$frgfRDtgRGyPN4xfJyISTeTEq4cWBUPgkTvRfK2QeLoSbdwq8AWRC", "Test", "USER" },
                    { 2, "adminemail@gmail.com", null, "$2a$11$zAwJ7jx0yYxAfLPqFu8FMuWDWT1gt.RNOK/e/odOob2HXOdJgeyq6", "Test", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 5, 24, 15, 25, 40, 258, DateTimeKind.Utc).AddTicks(6871), new DateTime(2023, 5, 24, 15, 20, 40, 258, DateTimeKind.Utc).AddTicks(6870) });
        }
    }
}
