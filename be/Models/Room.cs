using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int RoomNumber { get; set; } 
        public string Description { get; set; } = string.Empty;
        public int Rating { get; set; }
        public List<BookingRoom> BookingRooms { get; set; } = new List<BookingRoom>();
        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; } = new RoomType();

    }
}