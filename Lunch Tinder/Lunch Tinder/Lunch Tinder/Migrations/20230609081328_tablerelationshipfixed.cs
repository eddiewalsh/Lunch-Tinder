using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class tablerelationshipfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_LunchGroups_LunchGroupsGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LunchGroupsGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LunchGroupsGroupId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "LunchGroupUser",
                columns: table => new
                {
                    LunchGroupsGroupId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchGroupUser", x => new { x.LunchGroupsGroupId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_LunchGroupUser_LunchGroups_LunchGroupsGroupId",
                        column: x => x.LunchGroupsGroupId,
                        principalTable: "LunchGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LunchGroupUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 6, 9, 8, 18, 28, 345, DateTimeKind.Utc).AddTicks(4783), new DateTime(2023, 6, 9, 8, 13, 28, 345, DateTimeKind.Utc).AddTicks(4782) });

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
                name: "IX_LunchGroupUser_UsersUserId",
                table: "LunchGroupUser",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LunchGroupUser");

            migrationBuilder.AddColumn<int>(
                name: "LunchGroupsGroupId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 6, 2, 10, 8, 53, 961, DateTimeKind.Utc).AddTicks(1215), new DateTime(2023, 6, 2, 10, 3, 53, 961, DateTimeKind.Utc).AddTicks(1213) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "LunchGroupsGroupId", "Password" },
                values: new object[] { null, "$2a$11$T0H6e5kcJ8mtvIsj82UNbOuM1wxz0BofNXAlQ4KAE5JjQFQjmScPS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "LunchGroupsGroupId", "Password" },
                values: new object[] { null, "$2a$11$afYjpOPoFDpSJA/VnyVtEer0qeO5wHh9socqLJEMn1d/bHUsakPLa" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LunchGroupsGroupId",
                table: "Users",
                column: "LunchGroupsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LunchGroups_LunchGroupsGroupId",
                table: "Users",
                column: "LunchGroupsGroupId",
                principalTable: "LunchGroups",
                principalColumn: "GroupId");
        }
    }
}
