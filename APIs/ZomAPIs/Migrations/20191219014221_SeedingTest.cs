using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomAPIs.Migrations
{
    public partial class SeedingTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RestaurantInfos",
                columns: new[] { "ResInfoId", "RestaurantHashId", "RestaurantMongoId" },
                values: new object[] { 0, 7656439828L, "5dede56b4a22d82ed1e66095" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FName", "LName", "Password", "Role", "ScreenName" },
                values: new object[] { 0, "junhanliu.leon@gmail.com", "Junhan", "Liu", "Seawave@007", "Manager", "Zayhan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RestaurantInfos",
                keyColumn: "ResInfoId",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 0);
        }
    }
}
