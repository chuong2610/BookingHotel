using BookingHotel.Models.Request;

namespace BookingHotel.Interfaces
{
    public interface IAuthService
    {
       Task<string> Login(LoginRequest loginRequest);
    }
}