    using BookingHotel.Interfaces;
    using Microsoft.AspNetCore.Mvc;
namespace BookingHotel.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotel()
        {
            var hotel = await _hotelService.GetHotel();
            if (hotel == null)
            {
                return BadRequest("Hotel not found");
            }
            return Ok(hotel);
        }
    }
}