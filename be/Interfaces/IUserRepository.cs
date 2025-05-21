using BookingHotel.Models;

namespace BookingHotel.Interfaces
{
    public interface IUserRepository
    {
        // Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}