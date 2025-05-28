using BookingHotel.Models;

namespace BookingHotel.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room?> GetByRoomNumberAsync(int roomNumber);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(int id, Room room);
        Task<bool> DeleteRoomAsync(int ind);
        Task<bool> RoomNumberExistsAsync(int roomNumber);
    }
}