using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Naam")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Adres")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Adres { get; set; }
        [Display(Name = "Adres")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Voeg een telefoonnummer als volgt toe: 0612345678")]
        public string PhoneNumber { get; set; }
    }
}
