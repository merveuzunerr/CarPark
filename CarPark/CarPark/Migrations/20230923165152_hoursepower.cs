using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPark.Migrations
{
    /// <inheritdoc />
    public partial class hoursepower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kilowatt",
                table: "HorsePowers");

            migrationBuilder.AddColumn<int>(
                name: "Kilowatt",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kilowatt",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "Kilowatt",
                table: "HorsePowers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
