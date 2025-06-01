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
            DateTime checkIn,
            DateTime checkOut,
            int? requiredPeople = null,
            int? childrenAllowed = null,
            List<string>? codes = null,
            decimal? minPricePerNight = null,
            decimal? maxPricePerNight = null
            )
        {
            // 1. Lấy danh sách phòng đang hoạt động và chưa bị đặt trong khoảng thời gian
            var availableRooms = _context.Rooms
                .Where(r => !_context.BookingRooms.Any(br =>
                        br.RoomId == r.Id &&
                        br.Booking.CheckInDate < checkOut &&
                        br.Booking.CheckOutDate > checkIn
                    ));

            // 2. Lấy các RoomType có ít nhất 1 phòng trống
            var availableRoomTypesQuery = availableRooms
                .GroupBy(r => r.RoomType)
                .Select(g => g.Key)
                .AsQueryable();

            // 3. Áp dụng điều kiện lọc theo số người lớn (MaxOccupancy) nếu có
            if (requiredPeople.HasValue)
            {
                availableRoomTypesQuery = availableRoomTypesQuery
                    .Where(rt => rt.MaxOccupancy >= requiredPeople.Value);
            }

            // 4. Áp dụng điều kiện lọc theo số trẻ em được phép nếu có
            if (childrenAllowed.HasValue)
            {
                availableRoomTypesQuery = availableRoomTypesQuery
                    .Where(rt => rt.ChildrenAllowed >= childrenAllowed.Value);
            }
            if (codes != null && codes.Any())
            {
                availableRoomTypesQuery = availableRoomTypesQuery
                    .Where(rt => codes.Contains(rt.Code));
            }

            if (minPricePerNight.HasValue)
            {
                availableRoomTypesQuery = availableRoomTypesQuery
                    .Where(rt => rt.Price >= minPricePerNight.Value);
            }

            if (maxPricePerNight.HasValue)
            {
                availableRoomTypesQuery = availableRoomTypesQuery
                    .Where(rt => rt.Price <= maxPricePerNight.Value);
            }

            // 5. Trả về danh sách RoomType còn phòng trống
            return await availableRoomTypesQuery.ToListAsync();
        }
    }

}