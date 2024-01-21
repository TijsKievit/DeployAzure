using Microsoft.AspNetCore.Identity;

namespace BeestjeOpJeFeestje.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Adres { get; set; }
        public string Card { get; set; }

    }
}
