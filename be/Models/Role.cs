namespace BookingHotel.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
       public List<User> Users{get; set;} = new List<User>();
        
        
    }
}