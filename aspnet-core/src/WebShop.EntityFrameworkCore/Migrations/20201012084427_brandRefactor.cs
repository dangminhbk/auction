﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class brandRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Brands");
        }
    }
}
