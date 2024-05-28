using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_LunchGroups_GroupId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventRestaurant");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Events_GroupId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "Votes",
                keyColumn: "VoteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Votes",
                keyColumn: "VoteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LunchGroups",
                keyColumn: "GroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LunchGroups",
                keyColumn: "GroupId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "LunchGroupsGroupId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "EventsEventId",
                table: "LunchGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes",
                column: "UserVoteID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LunchGroupsGroupId",
                table: "Users",
                column: "LunchGroupsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_EventsEventId",
                table: "Restaurants",
                column: "EventsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantsRestaurantID",
                table: "Restaurants",
                column: "RestaurantsRestaurantID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LunchGroups_LunchGroupsGroupId",
                table: "Users",
                column: "LunchGroupsGroupId",
                principalTable: "LunchGroups",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LunchGroups_Events_EventsEventId",
                table: "LunchGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Events_EventsEventId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Restaurants_RestaurantsRestaurantID",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LunchGroups_LunchGroupsGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Users_LunchGroupsGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_EventsEventId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantsRestaurantID",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_LunchGroups_EventsEventId",
                table: "LunchGroups");

            migrationBuilder.DropColumn(
                name: "LunchGroupsGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EventsEventId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantsRestaurantID",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "EventsEventId",
                table: "LunchGroups");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EventRestaurant",
                columns: table => new
                {
                    EventsEventId = table.Column<int>(type: "int", nullable: false),
                    OptionsRestaurantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRestaurant", x => new { x.EventsEventId, x.OptionsRestaurantID });
                    table.ForeignKey(
                        name: "FK_EventRestaurant_Events_EventsEventId",
                        column: x => x.EventsEventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRestaurant_Restaurants_OptionsRestaurantID",
                        column: x => x.OptionsRestaurantID,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupsGroupId = table.Column<int>(type: "int", nullable: false),
                    MembersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => new { x.GroupsGroupId, x.MembersUserId });
                    table.ForeignKey(
                        name: "FK_GroupMembers_LunchGroups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "LunchGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Users_MembersUserId",
                        column: x => x.MembersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LunchGroups",
                columns: new[] { "GroupId", "Description", "GroupName" },
                values: new object[,]
                {
                    { 1, "Group 1 description", "Group1" },
                    { 2, "Group 2 description", "Group2" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantID", "RestaurantDescription", "RestaurantName" },
                values: new object[,]
                {
                    { 1, "Restaurant 1 description", "Restaurant1" },
                    { 2, "Restaurant 2 description", "Restaurant2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "user1@example.com", "password1", "user1" },
                    { 2, "user2@example.com", "password2", "user2" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "EndTime", "GroupId", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, "Event 1 description", new DateTime(2023, 5, 19, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9586), 1, "Event1", new DateTime(2023, 5, 18, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9551) },
                    { 2, "Event 2 description", new DateTime(2023, 5, 19, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9593), 2, "Event2", new DateTime(2023, 5, 18, 16, 36, 51, 868, DateTimeKind.Local).AddTicks(9591) }
                });

            migrationBuilder.InsertData(
                table: "Votes",
                columns: new[] { "VoteId", "EventVoteID", "RestaurantVoteID", "UserVoteID" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes",
                column: "UserVoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_GroupId",
                table: "Events",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRestaurant_OptionsRestaurantID",
                table: "EventRestaurant",
                column: "OptionsRestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_MembersUserId",
                table: "GroupMembers",
                column: "MembersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_LunchGroups_GroupId",
                table: "Events",
                column: "GroupId",
                principalTable: "LunchGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
