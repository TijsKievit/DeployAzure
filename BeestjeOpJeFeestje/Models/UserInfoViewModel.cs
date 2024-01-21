using Microsoft.AspNetCore.Identity;

namespace BeestjeOpJeFeestje.Models
{
    public class UserInfoViewModel
    {
        public User user { get; set; }
        public Booking booking { get; set; }
    }
}
