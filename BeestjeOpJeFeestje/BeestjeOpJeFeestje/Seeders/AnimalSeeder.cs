using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Seeders
{
    public class AnimalSeeder
    {
        private readonly BeestjeOpJeFeestjeContext _context;
        public AnimalSeeder(BeestjeOpJeFeestjeContext beestjeOpJeFeestjeContext)
        {
            _context = beestjeOpJeFeestjeContext;
        }

        public void SeedData()
        {
            List<Animal> animals = new List<Animal>()
            {
                new Animal { Name = "Aap", Type = "jungle",  Price = 30.00, ImageLink = "aap.jpg"},
                new Animal { Name = "Olifant", Type = "jungle",  Price = 80.00 , ImageLink = "olifant.jpg"},
                new Animal { Name = "Zebra", Type = "jungle",  Price = 50.00 , ImageLink = "zebra.jpg"},
                new Animal { Name = "Leeuw", Type = "jungle",  Price = 70.00, ImageLink = "leeuw.jpg"},
                new Animal { Name = "Hond", Type = "Boerderij",  Price = 10.00, ImageLink = "hond.jpg"},
                new Animal { Name = "Ezel", Type = "Boerderij",  Price = 25.00, ImageLink = "ezel.jpg"},
                new Animal { Name = "Koe", Type = "Boerderij",  Price = 20.00, ImageLink = "koe.jpg"},
                new Animal { Name = "Eend", Type = "Boerderij",  Price = 7.50, ImageLink = "eend.jpg"},
                new Animal { Name = "Kuiken", Type = "Boerderij",  Price = 5.00, ImageLink = "kuiken.jpg"},
                new Animal { Name = "Pinguïn", Type = "Sneeuw",  Price = 55.00, ImageLink = "pinguïn.jpg"},
                new Animal { Name = "IJsbeer", Type = "Sneeuw",  Price = 90.00, ImageLink = "ijsbeer.jpg"},
                new Animal { Name = "Zeehond", Type = "Sneeuw",  Price = 70.00, ImageLink = "zeehond.jpg"},
                new Animal { Name = "Kameel", Type = "Woestijn",  Price = 45.00, ImageLink = "kameel.jpg"},
                new Animal { Name = "Slang", Type = "Woestijn",  Price = 25.00, ImageLink = "slang.jpg"},
                new Animal { Name = "T-Rex", Type = "VIP",  Price = 250.00, ImageLink = "t-rex.jpg"},
                new Animal { Name = "Unicorn", Type = "VIP",  Price = 500.00, ImageLink = "unicorn.jpg"}
            };
            if (_context.Animals.ToList().Count == 0)
            {
                foreach (var animal in animals)
                {
                    _context.Add(animal);
                    _context.SaveChanges();
                }
            }
        }
    }
}

