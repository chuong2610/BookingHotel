using BookingHotel.Models.DTO;

namespace BookingHotel.Interfaces{
    public interface IHotelService{
        Task<HotelDTO> GetHotel();
    }
}