using BookingHotel.Data;
using BookingHotel.Interfaces;
using BookingHotel.Models;
using BookingHotel.Models.DTO;

namespace BookingHotel.Services
{
    public class RoomService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ApplicationDBContext _context;

        public RoomService(IRoomTypeRepository roomTypeRepository, ApplicationDBContext context)
        {
            _roomTypeRepository = roomTypeRepository;
            _context = context;
        }
        public async Task<List<RoomDTO>> GetAllRooms()
        {
            var roomTypes = await _roomTypeRepository.GetAllRoomTypes();
            return roomTypes.Select(rt => MapToRoomDTO(rt)).ToList();
        }

        public async Task<RoomDetailDTO> GetRoomById(int id)
        {
            var roomType = await _roomTypeRepository.GetRoomTypeById(id);
            if (roomType == null)
            {
                return null; // or throw an exception
            }
            var emptyRoomsCount = roomType.Rooms.Count(r =>
                !_context.BookingRooms.Any(br =>
                br.RoomId == r.Id 
                )   
            );

            return new RoomDetailDTO
            {
                Name = roomType.Type,
                SummaryDescription = roomType.SummaryDescription,
                Imgs = roomType.Images.Select(i => "http://localhost:5178/uploads/" + i.Img).ToList(),
                Rating = (int)roomType.Rooms.Average(r => r.Rating),
                Description = roomType.Description,
                PricePerNight = (int)roomType.Price,
                Code = roomType.Code,
                MaxOccupancy = roomType.MaxOccupancy,
                ChildrenAllowed = roomType.ChildrenAllowed,
                EmptyRooms = emptyRoomsCount 
            };
        }

        public async Task<List<RoomDTO>> GetAvailableRooms(
            DateTime? checkIn,
            DateTime? checkOut,
            int? requiredPeople = null,
            int? childrenAllowed = null,
            List<string>? codes = null,
            decimal? minPricePerNight = null,
            decimal? maxPricePerNight = null
            )
        {
            var availableRoomTypes = await _roomTypeRepository.GetAvailableRoomTypes(checkIn, checkOut, requiredPeople, childrenAllowed, codes, minPricePerNight, maxPricePerNight);
            return availableRoomTypes.Select(rt => MapToRoomDTO(rt)).ToList();
        }


        private RoomDTO MapToRoomDTO(RoomType roomType)
        {
            return new RoomDTO
            {
                Id = roomType.Id,
                Name = roomType.Type,
                SummaryDescription = roomType.SummaryDescription,
                Img ="http://localhost:5178/uploads/"+ roomType.Images.FirstOrDefault()?.Img ?? string.Empty,
                Rating = (int)roomType.Rooms.Average(r => r.Rating),
                PricePerNight = (int)roomType.Price,
                MaxOccupancy = roomType.MaxOccupancy,
                ChildrenAllowed = roomType.ChildrenAllowed
            };
        }

    }
}