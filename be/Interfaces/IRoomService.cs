using BookingHotel.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingHotel.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetTopRatedRoomsAsync(int count);
    }
}