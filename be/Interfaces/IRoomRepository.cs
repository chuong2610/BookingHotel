using BookingHotel.Models;

namespace BookingHotel.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRooms();
    }
}