using System.Collections.Generic;

namespace BeestjeOpJeFeestje.Models
{
    public class ConfirmBookingViewModel
    {
        public List<Animal> animals {  get; set; }
        public List<Discount> discounts { get; set; }
        public double totalPrice { get; set; }
        public int totalDiscount { get; set; }
        public Booking booking { get; set; }
    }
}
