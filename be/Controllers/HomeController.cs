using BookingHotel.Interfaces;
using BookingHotel.Models.DTO;
using BookingHotel.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;
        private readonly IRoomService _roomService;

        public HomeController(IHomeService homeService, IRoomService roomService)
        {
            _homeService = homeService;
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHomeData()
        {
            try
            {
                var banner = await _homeService.GetBannerAsync();
                var about = await _homeService.GetAboutAsync();
                var services = await _homeService.GetServicesAsync();
                var topRatedRooms = await _roomService.GetTopRatedRoomsAsync(3);
                var reviews = await _homeService.GetReviewsAsync();
                var news = await _homeService.GetLatestNewsAsync();

                return Ok(new
                {
                    banner,
                    about,
                    services,
                    topRatedRooms,
                    reviews,
                    news
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("banner")]
        public async Task<IActionResult> GetBanner()
        {
            try
            {
                var banner = await _homeService.GetBannerAsync();
                return Ok(banner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("about")]
        public async Task<IActionResult> GetAbout()
        {
            try
            {
                var about = await _homeService.GetAboutAsync();
                return Ok(about);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetServices()
        {
            try
            {
                var services = await _homeService.GetServicesAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("top-rated-rooms")]
        public async Task<IActionResult> GetTopRatedRooms([FromQuery] int count = 3)
        {
            try
            {
                var rooms = await _roomService.GetTopRatedRoomsAsync(count);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var reviews = await _homeService.GetReviewsAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                var news = await _homeService.GetLatestNewsAsync();
                return Ok(news);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}