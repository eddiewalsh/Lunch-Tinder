using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class InviteToLunchGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvitesLG",
                columns: table => new
                {
                    InviteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LunchGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsernameEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitesLG", x => x.InviteID);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 6, 14, 14, 1, 54, 427, DateTimeKind.Utc).AddTicks(9215), new DateTime(2023, 6, 14, 13, 56, 54, 427, DateTimeKind.Utc).AddTicks(9214) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$JCnfsLirY4zZj2s6zbpP5.YhyoU3xgzOSd6m/S6Sv4FJvUIK0HCN.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$96cnvKoTj.81a5sTLuVqKO4cXOmAOWwciT8OnHOud5oVpFfA0iuFC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvitesLG");

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
    }
}
