using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class addauction2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AbpUsers_WinnerId",
                table: "Auctions");

            migrationBuilder.AlterColumn<long>(
                name: "WinnerId",
                table: "Auctions",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AbpUsers_WinnerId",
                table: "Auctions",
                column: "WinnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AbpUsers_WinnerId",
                table: "Auctions");

            migrationBuilder.AlterColumn<long>(
                name: "WinnerId",
                table: "Auctions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AbpUsers_WinnerId",
                table: "Auctions",
                column: "WinnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
