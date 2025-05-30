namespace BookingHotel.Models.Request
{
    public class SearchRequest
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public int? NumberOfBed { get; set; }
        public int? MinPricePerNight { get; set; }
        public int? MaxPricePerNight { get; set; }

    }
}