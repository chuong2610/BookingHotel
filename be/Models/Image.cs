namespace BookingHotel.Models{
    public class Image{
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = new Hotel();
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; } = new RoomType();
        public int RoomId { get; set; }
        public Room Room { get; set; } = new Room();
    }
}