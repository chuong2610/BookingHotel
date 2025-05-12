using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int RoomNumber { get; set; } 
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; } = new RoomType();

    }
}