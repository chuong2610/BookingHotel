using BookingHotel.Data;
using BookingHotel.Interfaces;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }
    }

}