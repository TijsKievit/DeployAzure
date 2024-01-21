using System.Collections.Generic;

namespace BeestjeOpJeFeestje.Models
{
    public class AnimalDetailsViewModel
    {
        public Animal animal {  get; set; }
        public List<Booking> bookings { get; set; }
        public int selectedBookingId { get; set; }
    }
}
