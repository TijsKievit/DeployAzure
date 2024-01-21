using Microsoft.AspNetCore.Mvc;
using BeestjeOpJeFeestje.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BeestjeOpJeFeestje.Controllers
{
    public class DiscountController : Controller
    {
        private List<Animal> animals;
        private User user;
        private Booking booking;
        private List<Discount> discounts;
        public DiscountController(List<Animal> animals, User user, Booking booking) 
        {
            this.animals = animals;
            this.user = user;
            this.booking = booking;
            discounts = new List<Discount>();
        }

        public List<Discount> CreateDiscounts()
        {
            if (CheckThreeSameType())
            {
                discounts.Add(new Discount("3 Types", 10));
            }
            if (CheckDuck())
            {
                discounts.Add(new Discount("Eend", 50));
            }
            if (CheckMondayTuesday())
            {
                discounts.Add(new Discount("Maandag Dinsdag", 15));
            }
            if (CheckAlphabet() != 0)
            {
                var discount = new Discount("Alfabet", CheckAlphabet());
                discounts.Add(discount);
            }
            if (CheckCustomerCard())
            {
                discounts.Add(new Discount("Klantenkaart", 10));
            }
            return discounts;
        }

        public double returnTotalPrice()
        {
            double totalPrice = 0;
            foreach (var animal in animals)
            {
                totalPrice += animal.Price;
            }
            return totalPrice;
        }

        public int ReturnTotalDiscount()
        {
            int totalDiscount = 0;
            foreach(var discount in discounts)
            {
                totalDiscount += discount.Percentage;
            }
            if(totalDiscount > 60)
            {
                totalDiscount = 60;
            }
            return totalDiscount;
        }

        private bool CheckCustomerCard()
        {
            if(user.Card != null)
            {
                return true;
            }
            return false;
        }

        private int CheckAlphabet()
        {
            int highestDiscount = 0;
            bool isActive = true;
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            foreach(char c in alphabet)
            {
                
                if (isActive)
                {
                    bool foundLetter = false;
                    foreach (var animal in animals)
                    {
                        if (!foundLetter)
                        {
                            if (animal.Name.Contains(c))
                            {
                                highestDiscount += 2;
                                foundLetter = true;
                            }
                        }
                    }
                    if (!foundLetter) 
                    {
                        isActive = false;
                    }
                }
                
            }
            return highestDiscount;
        }

        private bool CheckMondayTuesday()
        {
            if(booking.Date.DayOfWeek == DayOfWeek.Monday || booking.Date.DayOfWeek == DayOfWeek.Tuesday)
            {
                return true;
            }
            return false;
        }

        private bool CheckDuck()
        {
            if(animals.FirstOrDefault(a => a.Name == "Eend") != null && booking.chickenDiscount == 0)
            {
                if (CheckGamble() || booking.chickenDiscount == 2)
                {
                    if(booking.chickenDiscount == 1)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool CheckGamble()
        {
            Random random = new Random();

            int randomNumber = random.Next(1, 7);
            if (randomNumber == 1) 
            {
                return true;
            }
            return false;
        }

        private bool CheckThreeSameType()
        {
            foreach(var animal in animals)
            {
                if(animals.Where(a => a.Type == animal.Type).Count() >= 3)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
