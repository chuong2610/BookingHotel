using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfBed",
                table: "RoomTypes");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "RoomTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "RoomTypes");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBed",
                table: "RoomTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
