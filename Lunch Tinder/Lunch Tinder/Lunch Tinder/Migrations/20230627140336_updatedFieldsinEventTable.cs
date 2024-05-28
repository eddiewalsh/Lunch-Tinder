using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class updatedFieldsinEventTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Events",
                newName: "VotingStartTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Events",
                newName: "VotingEndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEndTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventStartTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$/1Z1IGaxnzJBSojSkfAhQeNf4XJIcaAmqSIjVzdfXcSjliNg8dV.a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$lhhYgQXADjq0cyUaNza0.udO.QKuiDeNqQopJniLIqiMSOO57sXyW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEndTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventStartTime",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "VotingStartTime",
                table: "Events",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "VotingEndTime",
                table: "Events",
                newName: "EndTime");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "EndTime", "Name", "StartTime" },
                values: new object[] { 1, "Great Burgers", new DateTime(2023, 6, 15, 16, 22, 5, 804, DateTimeKind.Utc).AddTicks(8496), "Locke Burger", new DateTime(2023, 6, 15, 16, 17, 5, 804, DateTimeKind.Utc).AddTicks(8491) });

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
        }
    }
}
