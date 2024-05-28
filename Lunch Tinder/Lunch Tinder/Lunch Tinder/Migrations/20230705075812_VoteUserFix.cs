using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunch_Tinder.Migrations
{
    /// <inheritdoc />
    public partial class VoteUserFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes");

            migrationBuilder.AddColumn<string>(
                name: "VenueWinner",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ii/WP4wR8VgrIUqRwkhXheyGwJxJhLwlq5K8dSbWKw3OVTlXoCb..");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$k0NMQm2Jm8l3KGSM2xzToOX5taUJilEf7CIvYUQAyjX79jj1z4Xwq");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes",
                column: "UserVoteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "VenueWinner",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$l0ElG/erYpkJGWGPfgG49uRWmCF3v8vYdSPGWMZnuyJ2FI0kJTtDq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$nyJCea0FuDHqO1hoCGlT4.A/2333TpEDAUHgDTkO59Dmv//j9goeG");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserVoteID",
                table: "Votes",
                column: "UserVoteID",
                unique: true);
        }
    }
}
