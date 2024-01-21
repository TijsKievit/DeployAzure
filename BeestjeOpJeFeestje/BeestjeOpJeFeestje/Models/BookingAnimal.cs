using System;
using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Models
{
    public class BookingAnimal
    {
        [Key]
        public int id { get; set; }
        public int animalId { get; set; }
        public int bookingId { get; set; }
    }
}
