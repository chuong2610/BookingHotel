using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookingHotel.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
         [JsonIgnore]
       public List<User> Users { get; set; } = new List<User>();
        
        
    }
}