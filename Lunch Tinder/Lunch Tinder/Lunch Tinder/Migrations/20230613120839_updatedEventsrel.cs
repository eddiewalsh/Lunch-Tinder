using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class updatedEventsrel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LunchGroups_Events_EventsEventId",
                table: "LunchGroups");

            migrationBuilder.DropIndex(
                name: "IX_LunchGroups_EventsEventId",
                table: "LunchGroups");

            migrationBuilder.DropColumn(
                name: "EventsEventId",
                table: "LunchGroups");

            migrationBuilder.CreateTable(
                name: "EventLunchGroup",
                columns: table => new
                {
                    EventsEventId = table.Column<int>(type: "int", nullable: false),
                    LunchGroupsGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLunchGroup", x => new { x.EventsEventId, x.LunchGroupsGroupId });
                    table.ForeignKey(
                        name: "FK_EventLunchGroup_Events_EventsEventId",
                        column: x => x.EventsEventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventLunchGroup_LunchGroups_LunchGroupsGroupId",
                        column: x => x.LunchGroupsGroupId,
                        principalTable: "LunchGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_EventLunchGroup_LunchGroupsGroupId",
                table: "EventLunchGroup",
                column: "LunchGroupsGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventLunchGroup");

            migrationBuilder.AddColumn<int>(
                name: "EventsEventId",
                table: "LunchGroups",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 6, 9, 8, 18, 28, 345, DateTimeKind.Utc).AddTicks(4783), new DateTime(2023, 6, 9, 8, 13, 28, 345, DateTimeKind.Utc).AddTicks(4782) });

            migrationBuilder.UpdateData(
                table: "LunchGroups",
                keyColumn: "GroupId",
                keyValue: 1,
                column: "EventsEventId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$KgVBHdeNQ/Q6QK.AUfUmJ.GzcMGoUrpx6is63EdBytLlhZmJHyT3m");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$HTOGSWz75JosOB02ziODZuXNk0W2dsyp0TOesqUI9TEykPlvgzJvW");

            migrationBuilder.CreateIndex(
                name: "IX_LunchGroups_EventsEventId",
                table: "LunchGroups",
                column: "EventsEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_LunchGroups_Events_EventsEventId",
                table: "LunchGroups",
                column: "EventsEventId",
                principalTable: "Events",
                principalColumn: "EventId");
        }
    }
}
