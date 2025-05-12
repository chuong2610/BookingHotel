using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public Paymet Paymet { get; set; } = new Paymet();
        public Review Review { get; set; } = new Review();
        public List<Room> Rooms { get; set; } = new List<Room>();
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = new User();
      
    }
}