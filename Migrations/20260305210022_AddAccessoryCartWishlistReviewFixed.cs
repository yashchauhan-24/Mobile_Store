using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile_Store.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessoryCartWishlistReviewFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Wishlists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MobileAccessoryId",
                table: "Wishlists",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MobileAccessoryId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessoryReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsVerifiedPurchase = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoryReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessoryReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessoryReviews_MobileAccessories_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "MobileAccessories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_MobileAccessoryId",
                table: "Wishlists",
                column: "MobileAccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MobileAccessoryId",
                table: "CartItems",
                column: "MobileAccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessoryReviews_AccessoryId",
                table: "AccessoryReviews",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessoryReviews_UserId",
                table: "AccessoryReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_MobileAccessories_MobileAccessoryId",
                table: "CartItems",
                column: "MobileAccessoryId",
                principalTable: "MobileAccessories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_MobileAccessories_MobileAccessoryId",
                table: "Wishlists",
                column: "MobileAccessoryId",
                principalTable: "MobileAccessories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_MobileAccessories_MobileAccessoryId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_MobileAccessories_MobileAccessoryId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists");

            migrationBuilder.DropTable(
                name: "AccessoryReviews");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_MobileAccessoryId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_MobileAccessoryId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "MobileAccessoryId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "MobileAccessoryId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Wishlists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
