using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models.DTOs
{
    public class RoomDTO
    {
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
    }
    public class RoomResponseDTO
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } // Thêm thông tin từ RoomType
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

    
}