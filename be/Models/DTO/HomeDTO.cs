namespace BookingHotel.Models.DTO
{
    public class HomeDto
    {
        public BannerDto? Banner { get; set; }
        public AboutDto? About { get; set; }
        public List<ServiceDto>? Services { get; set; }
        public List<RoomDto>? TopRatedRooms { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
        public List<NewsDto>? News { get; set; }
    }

    public class BannerDto
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
    }

    public class AboutDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
    }

    public class ServiceDto
    {
        public int Id { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class RoomDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? ShortDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Currency { get; set; }
        public double Rating { get; set; }
        public string? BookNowUrl { get; set; }
        public string? DetailsUrl { get; set; }
    }

    public class ReviewDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Country { get; set; }
        public string? Avatar { get; set; }
        public string? Content { get; set; }
    }

    public class NewsDto
    {
        public int Id { get; set; }
        public string? Thumbnail { get; set; }
        public string? Title { get; set; }
        public string? Excerpt { get; set; }
        public string? ReadMoreUrl { get; set; }
    }
}