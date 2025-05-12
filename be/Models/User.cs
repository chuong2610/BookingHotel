using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookingHotel.Models{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Review> Reviews { get; set; } = new List<Review>();
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; } = new Role();
       
        
    }
}