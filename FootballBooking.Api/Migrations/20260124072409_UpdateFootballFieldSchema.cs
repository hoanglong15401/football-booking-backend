using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBooking.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFootballFieldSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "CloseTime",
                table: "FootballFields",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FootballFields",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FieldType",
                table: "FootballFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FootballFields",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpenTime",
                table: "FootballFields",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "FootballFields",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "FootballFields");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FootballFields");

            migrationBuilder.DropColumn(
                name: "FieldType",
                table: "FootballFields");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FootballFields");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "FootballFields");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "FootballFields");
        }
    }
}
