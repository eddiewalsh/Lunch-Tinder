using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class EventLunchRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Events_EventsEventId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Restaurants_RestaurantsRestaurantID",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_EventsEventId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantsRestaurantID",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "EventsEventId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantsRestaurantID",
                table: "Restaurants");

            migrationBuilder.CreateTable(
                name: "EventRestaurant",
                columns: table => new
                {
                    EventsEventId = table.Column<int>(type: "int", nullable: false),
                    RestaurantOptionsRestaurantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRestaurant", x => new { x.EventsEventId, x.RestaurantOptionsRestaurantID });
                    table.ForeignKey(
                        name: "FK_EventRestaurant_Events_EventsEventId",
                        column: x => x.EventsEventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRestaurant_Restaurants_RestaurantOptionsRestaurantID",
                        column: x => x.RestaurantOptionsRestaurantID,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 6, 15, 16, 22, 5, 804, DateTimeKind.Utc).AddTicks(8496), new DateTime(2023, 6, 15, 16, 17, 5, 804, DateTimeKind.Utc).AddTicks(8491) });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantID", "RestaurantDescription", "RestaurantName" },
                values: new object[,]
                {
                    { 1, "We don’t do burgers, we do Locke burgers.", "Locke Burger Castletroy" },
                    { 2, "Italian pizza and European dishes", "La Cucina Limerick" },
                    { 3, "Sandwhiches, Salads,Breakfast Foods", "Delish Cafe" },
                    { 4, "Hotel, Traditional Food", "McLaughlins Restaurant" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Wh0t3jRNHyZRGTsd6bO/veOwOA2hwVo3zqt7JsK8T4EB261e5ld9i");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$kaEvJe64ff4Jl.WonGDkAe3xWZBqrmRyG/DNGKDBPKf01iLtd4VlC");

            migrationBuilder.CreateIndex(
                name: "IX_EventRestaurant_RestaurantOptionsRestaurantID",
                table: "EventRestaurant",
                column: "RestaurantOptionsRestaurantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRestaurant");

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "EventsEventId",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantsRestaurantID",
                table: "Restaurants",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_EventsEventId",
                table: "Restaurants",
                column: "EventsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantsRestaurantID",
                table: "Restaurants",
                column: "RestaurantsRestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Events_EventsEventId",
                table: "Restaurants",
                column: "EventsEventId",
                principalTable: "Events",
                principalColumn: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Restaurants_RestaurantsRestaurantID",
                table: "Restaurants",
                column: "RestaurantsRestaurantID",
                principalTable: "Restaurants",
                principalColumn: "RestaurantID");
        }
    }
}
