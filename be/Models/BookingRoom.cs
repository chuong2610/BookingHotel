namespace BookingHotel.Models
{
    public class BookingRoom
    {
        public int RoomId { get; set; }
        public Room Room { get; set; } = new Room();
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = new Booking();
    }
}