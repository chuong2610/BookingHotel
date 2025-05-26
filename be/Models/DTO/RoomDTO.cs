using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models.DTOs
{
    public class RoomResponseDTO
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public List<ImageResponseDTO> Images { get; set; } = new List<ImageResponseDTO>();
        public List<BookingResponseDTO> Bookings { get; set; } = new List<BookingResponseDTO>();
        public int RoomTypeId { get; set; }
        public RoomTypeResponseDTO RoomType { get; set; }
    }

    public class RoomRequestDTO
    {
        [Required(ErrorMessage = "Room number is required")]
        [Range(1, 10000, ErrorMessage = "Room number must be between 1 and 10000")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Room type ID is required")]
        public int RoomTypeId { get; set; }
        
        // Optional fields for images (could be handled separately in reality)
        public List<ImageRequestDTO>? Images { get; set; }
    }

    // Supporting DTOs
    public class ImageResponseDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class ImageRequestDTO
    {
        [Required]
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        // Other booking properties...
    }

    public class RoomTypeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
    }
}