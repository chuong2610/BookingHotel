using BookingHotel.Models;

namespace BookingHotel.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<List<RoomType>> GetAllRoomTypes();
        Task<RoomType?> GetRoomTypeById(int id);
        Task<List<RoomType>> GetAvailableRoomTypes(
            DateTime checkIn,
            DateTime checkOut,
            int? requiredPeople = null,
            int? childrenAllowed = null,
            List<string>? codes = null,
            decimal? minPricePerNight = null,
            decimal? maxPricePerNight = null
            );
    }
}