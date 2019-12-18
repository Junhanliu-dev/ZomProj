using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomAPIs.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantInfos",
                columns: table => new
                {
                    ResInfoId = table.Column<int>(nullable: false),
                    RestaurantMongoId = table.Column<int>(nullable: false),
                    RestaurantHashId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantInfos", x => x.ResInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: false),
                    LName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ScreenName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRestaurant",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ResInfoId = table.Column<int>(nullable: false),
                    Liked = table.Column<bool>(nullable: false),
                    BeenThereCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRestaurant", x => new { x.UserId, x.ResInfoId });
                    table.ForeignKey(
                        name: "FK_UserRestaurant_RestaurantInfos_ResInfoId",
                        column: x => x.ResInfoId,
                        principalTable: "RestaurantInfos",
                        principalColumn: "ResInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRestaurant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRestaurant_ResInfoId",
                table: "UserRestaurant",
                column: "ResInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRestaurant");

            migrationBuilder.DropTable(
                name: "RestaurantInfos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
