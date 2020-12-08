using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebShop.Migrations
{
    public partial class seller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SellerCoverId",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SellerLogoId",
                table: "Sellers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SellerCover",
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
                    table.PrimaryKey("PK_SellerCover", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerCover_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerLogo",
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
                    table.PrimaryKey("PK_SellerLogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerLogo_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_SellerCoverId",
                table: "Sellers",
                column: "SellerCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_SellerLogoId",
                table: "Sellers",
                column: "SellerLogoId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerCover_ImageId",
                table: "SellerCover",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerLogo_ImageId",
                table: "SellerLogo",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_SellerCover_SellerCoverId",
                table: "Sellers",
                column: "SellerCoverId",
                principalTable: "SellerCover",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_SellerLogo_SellerLogoId",
                table: "Sellers",
                column: "SellerLogoId",
                principalTable: "SellerLogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_SellerCover_SellerCoverId",
                table: "Sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_SellerLogo_SellerLogoId",
                table: "Sellers");

            migrationBuilder.DropTable(
                name: "SellerCover");

            migrationBuilder.DropTable(
                name: "SellerLogo");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_SellerCoverId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_SellerLogoId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SellerCoverId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SellerLogoId",
                table: "Sellers");
        }
    }
}
