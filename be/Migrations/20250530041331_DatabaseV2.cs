using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotel.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Rooms_RoomId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Images",
                newName: "RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_RoomId",
                table: "Images",
                newName: "IX_Images_RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_RoomTypes_RoomTypeId",
                table: "Images",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_RoomTypes_RoomTypeId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "RoomTypeId",
                table: "Images",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_RoomTypeId",
                table: "Images",
                newName: "IX_Images_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Rooms_RoomId",
                table: "Images",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
