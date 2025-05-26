using BookingHotel.Data;
using BookingHotel.Interfaces;
using BookingHotel.Models.DTO;


namespace BookingHotel.Repositories
{
    // Repositories/RoomRepository.cs
public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Room> CreateAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }
    
    // Implement other methods...
}

}