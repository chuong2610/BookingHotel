namespace BookingHotel.Models.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SummaryDescription { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public int Rating { get; set; }
        public decimal PricePerNight { get; set; }
        public int MaxOccupancy { get; set; }
        public int ChildrenAllowed { get; set; }
    }    
}