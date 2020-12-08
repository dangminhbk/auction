using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebShop.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Sellers_SellerId",
                table: "Images");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Sellers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "SellerId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Identified",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BrandImageId",
                table: "Brands",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrandImages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ImageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandImageId",
                table: "Brands",
                column: "BrandImageId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandImages_ImageId",
                table: "BrandImages",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_BrandImages_BrandImageId",
                table: "Brands",
                column: "BrandImageId",
                principalTable: "BrandImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Sellers_SellerId",
                table: "Images",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AbpUsers_UserId",
                table: "Sellers",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_BrandImages_BrandImageId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Sellers_SellerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AbpUsers_UserId",
                table: "Sellers");

            migrationBuilder.DropTable(
                name: "BrandImages");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandImageId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Identified",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "BrandImageId",
                table: "Brands");

            migrationBuilder.AlterColumn<long>(
                name: "SellerId",
                table: "Images",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Sellers_SellerId",
                table: "Images",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
