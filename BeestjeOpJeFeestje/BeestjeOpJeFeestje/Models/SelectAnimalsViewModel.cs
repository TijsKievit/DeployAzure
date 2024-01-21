using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BeestjeOpJeFeestje.Models
{
    public class SelectAnimalsViewModel : IValidatableObject
    {
        public List<Animal> availableAnimals {  get; set; }
        public List<int> selectedAnimalIds { get; set; }
        public List<int> bookedAnimalIds { get; set; }
        public Booking booking { get; set; }
        public string card { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var list = selectedAnimalIds;
            yield return ValidateZeroSelections(list);
            yield return ValidateLionBear(list);
            yield return ValidatePinguïn(list);
            yield return ValidateDesert(list);
            yield return ValidateSnow(list);
            yield return ValidateCardNumber(list);
            yield return ValidateVIPCard(list);
            yield return ValidateLonelyDog(list);
        }
        private ValidationResult ValidateZeroSelections(List<int> list)
        {
            if (list != null)
            {
                if (list.Count == 0 || list == null)
                {
                    return new ValidationResult("Je moet 1 beestje kiezen");
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("Je moet 1 beestje kiezen");

        }

        private ValidationResult ValidateLionBear(List<int> list)
        {
            if(list.Contains(4) || list.Contains(11))
            {
                if(list.Contains(5) || list.Contains(6) || list.Contains(7) || list.Contains(8))
                {
                    return new ValidationResult("Als je een Leeuw of een IJsbeer kiest, dan kan je geen boerderijdieren kiezen.");
                }
            }
            return ValidationResult.Success;
        }

        private ValidationResult ValidatePinguïn(List<int> list)
        {
            if (list.Contains(10) && (booking.Date.DayOfWeek == DayOfWeek.Saturday || booking.Date.DayOfWeek == DayOfWeek.Sunday))
            {
                return new ValidationResult("Pinguïn mag niet in het weekend worden geboekt.");
            }

            return ValidationResult.Success;

        }

        private ValidationResult ValidateDesert(List<int> list)
        {
            if ((list.Contains(13)|| list.Contains(14)) && (booking.Date.Month == 10 || booking.Date.Month == 11 || booking.Date.Month == 12 || booking.Date.Month == 1 || booking.Date.Month == 2))
            {
                return new ValidationResult("Je mag in oktober tot februari geen woestijn beestjes bestellen");
            }

            return ValidationResult.Success;

        }

        private ValidationResult ValidateSnow(List<int> list)
        {
            if ((list.Contains(11) || list.Contains(12)) && (booking.Date.Month == 6 || booking.Date.Month == 7 || booking.Date.Month == 8 || booking.Date.Month == 1))
            {
                return new ValidationResult("Je mag in juni tot augustus geen Sneeuw beestjes bestellen");
            }

            return ValidationResult.Success;

        }

        private ValidationResult ValidateCardNumber(List<int> list)
        {
            if(card == null && list.Count > 3)
            {
                return new ValidationResult("Zonder klantenkaart mag je maar 3 beestjes bestellen");
            }
            if (card == "zilveren" && list.Count > 4)
            {
                return new ValidationResult("Met een zilveren klantenkaart mag je maar 4 beestjes bestellen");
            }

            return ValidationResult.Success;

        }

        private ValidationResult ValidateVIPCard(List<int> list)
        {
            if (card != "platina" && (list.Contains(16) || list.Contains(15)))
            {
                return new ValidationResult("Zonder platina klantenkaart mag je geen VIP beestjes bestellen");
            }

            return ValidationResult.Success;

        }
        private ValidationResult ValidateLonelyDog(List<int> list)
        {
            if (list.Contains(5) && list.Count() == 1)
            {
                 return new ValidationResult("Honden willen niet alleen op pad, selecteer minimaal nog een beestje");
            }

            return ValidationResult.Success;

        }

    }
}
