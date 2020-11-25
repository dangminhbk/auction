using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class invoice2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuctionId",
                table: "Invoices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Invoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AuctionId",
                table: "Invoices",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Auctions_AuctionId",
                table: "Invoices",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Auctions_AuctionId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_AuctionId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Invoices");
        }
    }
}
