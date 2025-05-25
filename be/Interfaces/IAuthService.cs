using BookingHotel.Models;
using BookingHotel.Models.Request;
using BookingHotel.Request;

namespace BookingHotel.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequest loginRequest);
        Task<User?> VerifyEmailAsync(string token);
        Task<User> CreateUserAsync(RegisterRequest user);
    }
}