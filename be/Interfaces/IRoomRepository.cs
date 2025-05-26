using BookingHotel.Models;

namespace BookingHotel.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();

        Task<Room> GetRoomByIdAsync(int id);

        //  define function CreateProductAsync
        Task<Room> CreateRoomAsync(Room room);

        //  define function UpdateProductAsync
        Task<Room> UpdateRoomAsync(int id, Room room);

        //  define function DeleteProductAsync
        Task<bool> DeleteRoomAsync(int ind);
    }
}