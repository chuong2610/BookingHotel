using BookingHotel.Interfaces;
using BookingHotel.Models.DTO;

namespace BookingHotel.Services{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<HotelDTO> GetHotel()
        {
            var hotel = await _hotelRepository.GetHotel();
            return new HotelDTO
            {
                Name = hotel.Name,
                Address = hotel.Address,
                Description = hotel.Description,
                ContactNumber = hotel.ContactNumber
            };
        }

       
    }
}