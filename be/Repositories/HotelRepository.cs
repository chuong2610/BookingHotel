using BookingHotel.Data;
using BookingHotel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Models.DTO
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDBContext _context;

        public HotelRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Hotel?> GetHotel(){
            return await _context.Hotels.FirstOrDefaultAsync();
        }
    }
}