using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LunchGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchGroups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_LunchGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "LunchGroups",
                        principalColumn: "GroupId",
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
                name: "Votes",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserVoteID = table.Column<int>(type: "int", nullable: false),
                    RestaurantVoteID = table.Column<int>(type: "int", nullable: false),
                    EventVoteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Votes_Events_EventVoteID",
                        column: x => x.EventVoteID,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Restaurants_RestaurantVoteID",
                        column: x => x.RestaurantVoteID,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserVoteID",
                        column: x => x.UserVoteID,
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
                name: "IX_EventRestaurant_OptionsRestaurantID",
                table: "EventRestaurant",
                column: "OptionsRestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_GroupId",
                table: "Events",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_MembersUserId",
                table: "GroupMembers",
                column: "MembersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_EventVoteID",
                table: "Votes",
                column: "EventVoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RestaurantVoteID",
                table: "Votes",
                column: "RestaurantVoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes",
                column: "UserVoteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRestaurant");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LunchGroups");
        }
    }
}
