using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 6, 14, 11, 9, 8, 390, DateTimeKind.Utc).AddTicks(4985), new DateTime(2023, 6, 14, 11, 4, 8, 390, DateTimeKind.Utc).AddTicks(4982) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$v1koGsyI6gemgs1oM5jqz.9/kUF2dy.n4oYnKfddjD6OBO0TZs/1q");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$oFgvjLYt2PQYJ8tg/YLoGukFlLHoE9y80QvuM.DpNUQmCkmPeNLUi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 6, 13, 12, 13, 38, 910, DateTimeKind.Utc).AddTicks(1814), new DateTime(2023, 6, 13, 12, 8, 38, 910, DateTimeKind.Utc).AddTicks(1809) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$0ZPPbJOL4v09UXIBYWWzk.JAYuqkYrVjcnkaOdVd.ToY3P3FHMA/y");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$YoF6gMiafgJbCawIiBzUhezCVJHNA0fLGqmvgb8VBe6BAVIvGAML6");
        }
    }
}
