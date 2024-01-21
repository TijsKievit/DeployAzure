using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Models
{
    public class User : IdentityUser
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "De naam mag alleen letters hebben")]
        [StringLength(100, ErrorMessage = "De naam mag niet langer dan 100 letters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Het adres mag niet meer dan 100 tekens")]
        public string Adres { get; set; }
        public string Card { get; set; }

    }
}
