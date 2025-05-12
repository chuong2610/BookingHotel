using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = new User();
        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; } = new Booking();
        
    }
}