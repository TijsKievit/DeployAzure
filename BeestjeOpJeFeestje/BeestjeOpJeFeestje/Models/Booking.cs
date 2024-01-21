using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Models
{
    public class Booking
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
    }
}
