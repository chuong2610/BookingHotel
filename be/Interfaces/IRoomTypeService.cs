using BookingHotel.Models;
using BookingHotel.Models.DTO;

namespace BookingHotel.Interfaces
{
    public interface IRoomTypeService
    {
        Task<List<RoomDTO>> GetAllRooms();
        Task<RoomDetailDTO> GetRoomById(int id);
        Task<List<RoomDTO>> GetAvailableRooms(
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