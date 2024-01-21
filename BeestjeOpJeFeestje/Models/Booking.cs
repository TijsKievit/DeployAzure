using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Models
{
    public class Booking : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Adres { get; set; }
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }
        public Boolean IsBevestigd { get; set; }
        public string UserId { get; set; }
        public int chickenDiscount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidateZeroSelections();
        }
        private ValidationResult ValidateZeroSelections()
        {
            DateTime currentDate = DateTime.Now;
            DateTime maxDate = currentDate.AddYears(3);

            if (Date >= currentDate && Date <= maxDate || Date == new DateTime(1, 1, 1, 0, 0, 0))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Vul een Datum in tussen nu en 3 jaar.");
            }

        }
    }
}
