using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomAPIs.Migrations
{
    public partial class changeMongodbIdFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurant_RestaurantInfos_ResInfoId",
                table: "UserRestaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurant_Users_UserId",
                table: "UserRestaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRestaurant",
                table: "UserRestaurant");

            migrationBuilder.DropIndex(
                name: "IX_UserRestaurant_ResInfoId",
                table: "UserRestaurant");

            migrationBuilder.RenameTable(
                name: "UserRestaurant",
                newName: "UserRestaurants");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RestaurantMongoId",
                table: "RestaurantInfos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "string");

            migrationBuilder.AlterColumn<long>(
                name: "RestaurantHashId",
                table: "RestaurantInfos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantInfoResInfoId",
                table: "UserRestaurants",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRestaurants",
                table: "UserRestaurants",
                columns: new[] { "UserId", "ResInfoId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurants_RestaurantInfos_RestaurantInfoResInfoId",
                table: "UserRestaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRestaurants_Users_UserId",
                table: "UserRestaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRestaurants",
                table: "UserRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_UserRestaurants_RestaurantInfoResInfoId",
                table: "UserRestaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantInfoResInfoId",
                table: "UserRestaurants");

            migrationBuilder.RenameTable(
                name: "UserRestaurants",
                newName: "UserRestaurant");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantMongoId",
                table: "RestaurantInfos",
                type: "string",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantHashId",
                table: "RestaurantInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRestaurant",
                table: "UserRestaurant",
                columns: new[] { "UserId", "ResInfoId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRestaurant_ResInfoId",
                table: "UserRestaurant",
                column: "ResInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRestaurant_RestaurantInfos_ResInfoId",
                table: "UserRestaurant",
                column: "ResInfoId",
                principalTable: "RestaurantInfos",
                principalColumn: "ResInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRestaurant_Users_UserId",
                table: "UserRestaurant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
