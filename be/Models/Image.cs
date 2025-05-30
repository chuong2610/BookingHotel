using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models{
    public class Image{
        [Key]
        public int Id { get; set; }
        public string Img { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; } = new RoomType();
    }
}