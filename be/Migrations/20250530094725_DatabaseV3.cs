using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "dateAvailable",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "EmptyRooms",
                table: "RoomTypes",
                newName: "NumberOfBed");

            migrationBuilder.AddColumn<int>(
                name: "ChildrenAllowed",
                table: "RoomTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildrenAllowed",
                table: "RoomTypes");

            migrationBuilder.RenameColumn(
                name: "NumberOfBed",
                table: "RoomTypes",
                newName: "EmptyRooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "dateAvailable",
                table: "Rooms",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
