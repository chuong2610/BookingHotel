using BookingHotel.Interfaces;
using BookingHotel.Models.DTO;
using BookingHotel.Models;

namespace BookingHotel.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            return rooms.Select(MapToRoomResponseDTO);
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            return MapToRoomResponseDTO(room);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            var room = new Room
            {
                RoomNumber = room.RoomNumber,
                RoomTypeId = roomRequestDTO.RoomTypeId,
                // Initialize empty collections
                Images = new List<Image>(),
                Bookings = new List<Booking>()
            };

            var createdRoom = await _roomRepository.CreateRoomAsync(room);
            return MapToRoomResponseDTO(createdRoom);
        }

        public async Task<Room?> UpdateRoomAsync(int id, Room room)
        {
            var room = new Room
            {
                RoomNumber = room.RoomNumber,
                RoomTypeId = room.RoomTypeId
                // Note: We typically don't update collections in a simple update
            };

            var updatedRoom = await _roomRepository.UpdateRoomAsync(id, room);
            return updatedRoom != null ? MapToRoomResponseDTO(updatedRoom) : null;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            return await _roomRepository.DeleteRoomAsync(id);
        }

       
    }
}