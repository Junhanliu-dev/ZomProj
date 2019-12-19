using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomAPIs.Migrations
{
    public partial class SeedingTest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRestaurants",
                columns: new[] { "UserId", "ResInfoId", "BeenThereCount", "Liked", "RestaurantInfoResInfoId" },
                values: new object[] { 0, 0, 1, true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRestaurants",
                keyColumns: new[] { "UserId", "ResInfoId" },
                keyValues: new object[] { 0, 0 });
        }
    }
}
