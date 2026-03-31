using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBooking.Api.Migrations
{
    public partial class AddDrinkToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Drink",
                table: "Bookings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Drink",
                table: "Bookings");
        }
    }
}