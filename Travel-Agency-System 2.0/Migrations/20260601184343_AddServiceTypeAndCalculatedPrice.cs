using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_System_2._0.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTypeAndCalculatedPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Trips");
        }
    }
}
