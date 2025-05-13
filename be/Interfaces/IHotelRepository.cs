using BookingHotel.Models;

namespace BookingHotel.Interfaces{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotel();
    }
}