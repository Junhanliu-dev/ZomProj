using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomAPIs.Migrations
{
    public partial class addRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurants_RestaurantInfos_RestaurantInfoResInfoId",
                table: "UserRestaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurants_Users_UserId",
                table: "UserRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_UserRestaurants_RestaurantInfoResInfoId",
                table: "UserRestaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantInfoResInfoId",
                table: "UserRestaurants");

            migrationBuilder.CreateIndex(
                name: "IX_UserRestaurants_ResInfoId",
                table: "UserRestaurants",
                column: "ResInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRestaurants_RestaurantInfos_ResInfoId",
                table: "UserRestaurants",
                column: "ResInfoId",
                principalTable: "RestaurantInfos",
                principalColumn: "ResInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRestaurants_Users_UserId",
                table: "UserRestaurants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurants_RestaurantInfos_ResInfoId",
                table: "UserRestaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurants_Users_UserId",
                table: "UserRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_UserRestaurants_ResInfoId",
                table: "UserRestaurants");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantInfoResInfoId",
                table: "UserRestaurants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRestaurants_RestaurantInfoResInfoId",
                table: "UserRestaurants",
                column: "RestaurantInfoResInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRestaurants_RestaurantInfos_RestaurantInfoResInfoId",
                table: "UserRestaurants",
                column: "RestaurantInfoResInfoId",
                principalTable: "RestaurantInfos",
                principalColumn: "ResInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRestaurants_Users_UserId",
                table: "UserRestaurants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
