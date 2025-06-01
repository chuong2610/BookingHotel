namespace BookingHotel.Models.DTO
{
    public class RoomDetailDTO
    {
        public string Name { get; set; } = string.Empty;
        public string SummaryDescription { get; set; } = string.Empty;
        public List<string> Imgs { get; set; } = new List<string>();
        public int Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public int PricePerNight { get; set; }
        public string Code { get; set; } = string.Empty;
        public int MaxOccupancy { get; set; }
        public int ChildrenAllowed { get; set; }
        public int EmptyRooms { get; set; }
    }
}