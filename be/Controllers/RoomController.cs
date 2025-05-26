using BookingHotel.Interfaces;
using BookingHotel.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingHotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomResponseDTO>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        // GET: api/Room/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomResponseDTO>> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            
            if (room == null)
            {
                return NotFound();
            }
            
            return Ok(room);
        }

        // POST: api/Room
        [HttpPost]
        public async Task<ActionResult<RoomResponseDTO>> CreateRoom(RoomRequestDTO roomDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRoom = await _roomService.CreateRoomAsync(roomDTO);
            return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
        }

        // PUT: api/Room/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResponseDTO>> UpdateRoom(int id, RoomRequestDTO roomDTO)
        {
            var updatedRoom = await _roomService.UpdateRoomAsync(id, roomDTO);
            
            if (updatedRoom == null)
            {
                return NotFound();
            }
            
            return Ok(updatedRoom);
        }

        // DELETE: api/Room/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}