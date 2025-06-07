using BookingHotel.Data;
using BookingHotel.Interfaces;
using BookingHotel.Models;
using BookingHotel.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomType>> GetAllRoomTypes()
        {
            return await _context.RoomTypes
                .Include(rt => rt.Rooms)
                .Include(rt => rt.Images)
                .ToListAsync();
        }


        public async Task<RoomType?> GetRoomTypeById(int id)
        {
            return await _context.RoomTypes
                 .Include(rt => rt.Rooms)
                .Include(rt => rt.Images)
                .FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<List<RoomType>> GetAvailableRoomTypes(
    DateTime? checkIn,
    DateTime? checkOut,
    int? requiredPeople = null,
    int? childrenAllowed = null,
    List<string>? codes = null,
    decimal? minPricePerNight = null,
    decimal? maxPricePerNight = null
)
{
    var roomTypes = _context.RoomTypes
        .Include(rt => rt.Rooms)
        .Include(rt => rt.Images)
        .AsQueryable();

    if (requiredPeople.HasValue)
    {
        roomTypes = roomTypes.Where(rt => rt.MaxOccupancy >= requiredPeople.Value);
    }

    if (childrenAllowed.HasValue)
    {
        roomTypes = roomTypes.Where(rt => rt.ChildrenAllowed >= childrenAllowed.Value);
    }

    if (codes != null && codes.Any())
    {
        roomTypes = roomTypes.Where(rt => codes.Contains(rt.Code));
    }

    if (minPricePerNight.HasValue)
    {
        roomTypes = roomTypes.Where(rt => rt.Price >= minPricePerNight.Value);
    }

    if (maxPricePerNight.HasValue)
    {
        roomTypes = roomTypes.Where(rt => rt.Price <= maxPricePerNight.Value);
    }

    // Kiểm tra mỗi RoomType có ít nhất một phòng trống
    if (checkIn.HasValue && checkOut.HasValue)
    {
        roomTypes = roomTypes.Where(rt =>
            rt.Rooms.Any(room =>
                !_context.BookingRooms.Any(br =>
                    br.RoomId == room.Id &&
                    br.Booking.CheckInDate < checkOut &&
                    br.Booking.CheckOutDate > checkIn
                )
            )
        );
    }

    return await roomTypes.ToListAsync();
}

    }

}