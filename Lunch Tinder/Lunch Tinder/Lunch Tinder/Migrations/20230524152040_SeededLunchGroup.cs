using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class SeededLunchGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 5, 24, 15, 25, 40, 258, DateTimeKind.Utc).AddTicks(6871), new DateTime(2023, 5, 24, 15, 20, 40, 258, DateTimeKind.Utc).AddTicks(6870) });

            migrationBuilder.InsertData(
                table: "LunchGroups",
                columns: new[] { "GroupId", "Description", "EventsEventId", "GroupName" },
                values: new object[] { 1, "We code", null, "Software Development" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LunchGroups",
                keyColumn: "GroupId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 5, 24, 15, 6, 51, 303, DateTimeKind.Utc).AddTicks(4662), new DateTime(2023, 5, 24, 15, 1, 51, 303, DateTimeKind.Utc).AddTicks(4660) });
        }
    }
}
