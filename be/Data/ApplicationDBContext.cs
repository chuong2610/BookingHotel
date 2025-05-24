using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }


        public DbSet<BookingHotel.Models.User> Users { get; set; }
        public DbSet<BookingHotel.Models.Hotel> Hotels { get; set; }
        public DbSet<BookingHotel.Models.Room> Rooms { get; set; }
        public DbSet<BookingHotel.Models.Image> Images { get; set; }
        public DbSet<BookingHotel.Models.Role> Roles { get; set; }
        public DbSet<BookingHotel.Models.Booking> Bookings { get; set; }
        public DbSet<BookingHotel.Models.RoomType> RoomTypes { get; set; }
        public DbSet<BookingHotel.Models.Paymet> Payments { get; set; }
        public DbSet<BookingHotel.Models.Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

    }
}