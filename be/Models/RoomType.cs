using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models{
    public class RoomType{
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Code { get; set; } = string.Empty;
        public int MaxOccupancy { get; set; }
        public int ChildrenAllowed { get; set; }
        public string SummaryDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}