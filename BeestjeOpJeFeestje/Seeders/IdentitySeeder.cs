﻿using Microsoft.AspNetCore.Identity;
using BeestjeOpJeFeestje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Seeders
{
    public class IdentitySeeder
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("Boer").Result == null)
            {
                User user = new()
                {
                    Email = "Boer@gmail.com",
                    UserName = "Boer@gmail.com",
                    Adres = "Dijkweg 20",
                    Name = "Jan Janssen",
                    PhoneNumber = "0647295824",
                    
                };

                IdentityResult result = userManager.CreateAsync(user, "Test1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Boer").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Boer").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Boer"
                };
                IdentityResult result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Klant").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Klant"
                };
                IdentityResult result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}

