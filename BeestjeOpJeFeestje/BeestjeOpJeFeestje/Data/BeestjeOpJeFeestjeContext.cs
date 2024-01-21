using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Data
{
    public class BeestjeOpJeFeestjeContext : IdentityDbContext<User, IdentityRole, string>
    {
        public BeestjeOpJeFeestjeContext (DbContextOptions<BeestjeOpJeFeestjeContext> options)
            : base(options)
        {
        }
        public DbSet<BeestjeOpJeFeestje.Models.Animal> Animals { get; set; }
        public DbSet<BeestjeOpJeFeestje.Models.User> Users { get; set; }
        public DbSet<BeestjeOpJeFeestje.Models.Booking> Bookings { get; set; }
        public DbSet<BeestjeOpJeFeestje.Models.BookingAnimal> bookingAnimals { get; set; }


    }
}
