using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class SeededEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "EndTime", "Name", "StartTime" },
                values: new object[] { 1, "Great Burgers", new DateTime(2023, 5, 24, 15, 6, 51, 303, DateTimeKind.Utc).AddTicks(4662), "Locke Burger", new DateTime(2023, 5, 24, 15, 1, 51, 303, DateTimeKind.Utc).AddTicks(4660) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);
        }
    }
}
