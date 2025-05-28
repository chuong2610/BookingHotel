using BookingHotel.Data;
using BookingHotel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Models.DTO
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDBContext _context;

        public RoomRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Images)
                .Include(r => r.Bookings)
                .ToListAsync();
        }
        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Images)
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Room?> GetByRoomNumberAsync(int roomNumber)
        {
            return await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(int id, Room room)
        {
            var existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom == null)
                return null;

            // Cập nhật các trường cần thiết
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.RoomTypeId = room.RoomTypeId;

            _context.Rooms.Update(existingRoom);
            await _context.SaveChangesAsync();
            return existingRoom;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RoomNumberExistsAsync(int roomNumber)
        {
            return await _context.Rooms
                .AnyAsync(r => r.RoomNumber == roomNumber);
        }

    }
}