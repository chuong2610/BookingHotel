using BookingHotel.Interfaces;
using BookingHotel.Models.DTO;
using BookingHotel.Models.Request;
using BookingHotel.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoomDTO>>> GetAllRooms()
        {
            try
            {
                var roomTypes = await _roomTypeService.GetAllRooms();
                return Ok(new BaseResponse<List<RoomDTO>>(true, "Lấy danh sách thành công", roomTypes));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>(false, $"Lỗi: {ex.Message}", null));
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDetailDTO>> GetRoomById(int id)
        {
            try
            {
                var room = await _roomTypeService.GetRoomById(id);
                if (room == null)
                {
                    return NotFound(new BaseResponse<string>(false, "Không tìm thấy phòng", null));
                }
                return Ok(new BaseResponse<RoomDetailDTO>(true, "Lấy thông tin phòng thành công", room));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>(false, $"Lỗi: {ex.Message}", null));
            }
        }

        [HttpPost("available")]
        public async Task<ActionResult<List<RoomDTO>>> GetAvailableRooms([FromBody] SearchRequest searchRequest)
        {
            try
            {


                var availableRooms = await _roomTypeService.GetAvailableRooms(
                    searchRequest.CheckInDate,
                    searchRequest.CheckOutDate,
                    searchRequest.NumberOfAdults,
                    searchRequest.NumberOfChildren,
                    searchRequest.Codes,
                    searchRequest.MinPricePerNight,
                    searchRequest.MaxPricePerNight
            );
            if (availableRooms == null || !availableRooms.Any())
            {
                return NotFound(new { success = false, message = "Không tìm thấy phòng phù hợp", data = (object?)null });
            }

                return Ok(new BaseResponse<List<RoomDTO>>(true, "Lấy danh sách phòng trống thành công", availableRooms));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>(false, $"Lỗi: {ex.Message}", null));
            }
        }





    }
}