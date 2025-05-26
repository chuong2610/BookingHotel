namespace BookingHotel.Models.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;

    }
     public class CreateRoomDTO
    {
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }

    }
}