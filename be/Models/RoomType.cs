using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models{
    public class RoomType{
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}