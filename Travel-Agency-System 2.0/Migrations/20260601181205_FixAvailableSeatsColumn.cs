using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_System_2._0.Migrations
{
    /// <inheritdoc />
    public partial class FixAvailableSeatsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "Trips");
        }
    }
}
