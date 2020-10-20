using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class seller2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentRegisterStatus",
                table: "Sellers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "SellerPaymentOptionId",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SellerPaymentOptions",
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
                    PaymentOption = table.Column<int>(nullable: false),
                    Payload = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPaymentOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_SellerPaymentOptionId",
                table: "Sellers",
                column: "SellerPaymentOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_SellerId",
                table: "AbpUsers",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Sellers_SellerId",
                table: "AbpUsers",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_SellerPaymentOptions_SellerPaymentOptionId",
                table: "Sellers",
                column: "SellerPaymentOptionId",
                principalTable: "SellerPaymentOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Sellers_SellerId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_SellerPaymentOptions_SellerPaymentOptionId",
                table: "Sellers");

            migrationBuilder.DropTable(
                name: "SellerPaymentOptions");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_SellerPaymentOptionId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_SellerId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "PaymentRegisterStatus",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SellerPaymentOptionId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "AbpUsers");
        }
    }
}
