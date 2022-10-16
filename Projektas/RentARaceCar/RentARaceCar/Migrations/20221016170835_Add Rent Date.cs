using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentARaceCar.Migrations
{
    public partial class AddRentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RentDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentDate",
                table: "Orders");
        }
    }
}
