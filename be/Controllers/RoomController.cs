using BookingHotel.Interfaces;
using BookingHotel.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(
        IRoomRepository roomRepository,
        ILogger<RoomsController> logger)
    {
        _roomRepository = roomRepository;
        _logger = logger;
    }

    // GET: api/Rooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomResponseDTO>>> GetAllRooms()
    {
        try
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            var roomDtos = rooms.Select(r => new RoomResponseDTO
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                RoomTypeId = r.RoomTypeId,
            //    ImageUrls = r.Images?.Select(i => i.Url).ToList() ?? new List<string>()
            }).ToList();

            return Ok(roomDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all rooms");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/Rooms/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomResponseDTO>> GetRoom(int id)
    {
        try
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            var roomDto = new RoomResponseDTO
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                RoomTypeId = room.RoomTypeId,
               // ImageUrls = room.Images?.Select(i => i.Url).ToList() ?? new List<string>()
            };

            return Ok(roomDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting room with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/Rooms
    [HttpPost]
    public async Task<ActionResult<RoomResponseDTO>> CreateRoom(RoomDTO roomDto)
    {
        try
        {
            if (await _roomRepository.RoomNumberExistsAsync(roomDto.RoomNumber))
            {
                return BadRequest("Room number already exists");
            }

            var room = new Models.Room
            {
                RoomNumber = roomDto.RoomNumber,
                RoomTypeId = roomDto.RoomTypeId
            };

            var createdRoom = await _roomRepository.CreateRoomAsync(room);

            var responseDto = new RoomResponseDTO
            {
                Id = createdRoom.Id,
                RoomNumber = createdRoom.RoomNumber,
                RoomTypeId = createdRoom.RoomTypeId,
            //    RoomTypeName = createdRoom.RoomType?.Name,
            //    ImageUrls = createdRoom.Images?.Select(i => i.Url).ToList() ?? new List<string>()
            };

            return CreatedAtAction(nameof(GetRoom), new { id = responseDto.Id }, responseDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating room");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/Rooms/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, RoomDTO roomDto)
    {
        try
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            // Check if room number is being changed to one that already exists
            if (existingRoom.RoomNumber != roomDto.RoomNumber && 
                await _roomRepository.RoomNumberExistsAsync(roomDto.RoomNumber))
            {
                return BadRequest("Room number already exists");
            }

            existingRoom.RoomNumber = roomDto.RoomNumber;
            existingRoom.RoomTypeId = roomDto.RoomTypeId;

            var updatedRoom = await _roomRepository.UpdateRoomAsync(id, existingRoom);
            if (updatedRoom == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating room with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/Rooms/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        try
        {
            var room = await _roomRepository.DeleteRoomAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting room with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }
}
}