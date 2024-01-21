using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using BeestjeOpJeFeestje.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BookingController : Controller
    {
        private readonly BeestjeOpJeFeestjeContext _context;
        private readonly UserManager<User> _userManager;
        public BookingController(BeestjeOpJeFeestjeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Boer, Klant")]
        // GET: BookingController
        public async Task<IActionResult> Index()
        {
            User user = GetAuthenticatedUser();
            return View(await _context.Bookings.Where(b => b.IsBevestigd == true && b.UserId == user.Id).ToListAsync());
        }

        // GET: BookingController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ConfirmBookingViewModel viewModel = GetConfirMBookingViewModel(id);

            return View(viewModel);
        }

        // GET: BookingController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            User user = GetAuthenticatedUser();
            booking.UserId = user.Id;
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("SelectAnimals", booking);
            }
            return View(booking);
        }

        public async Task<IActionResult> SelectAnimals(Booking booking)
        {
            var animalIds = GetAnimalIdsByDate(booking.Date);
            var user = GetAuthenticatedUser();

            var viewModel = new SelectAnimalsViewModel
            {
                availableAnimals = GetAvailableAnimals(),
                booking = booking,
                bookedAnimalIds = animalIds,
                card = user.Card
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectAnimals(SelectAnimalsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var selectedAnimalIds = viewModel.selectedAnimalIds;
                if (selectedAnimalIds != null)
                {
                    foreach (var animalId in selectedAnimalIds)
                    {
                        var animal = GetAnimalById(animalId);
                        var bookingAnimal = new BookingAnimal()
                        {
                            bookingId = viewModel.booking.Id,
                            animalId = animalId
                        };
                        _context.Add(bookingAnimal);
                    }
                    _context.SaveChanges();
                }

                return RedirectToAction("UserInfo", new { id = viewModel.booking.Id });
            }
            viewModel.availableAnimals = GetAvailableAnimals();
            viewModel.bookedAnimalIds = GetAnimalIdsByDate(viewModel.booking.Date);

            return View(viewModel);
                
        }

        public async Task<IActionResult> UserInfo(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            var animalIds = GetAnimalIdsByDate(booking.Date);
            User user = GetAuthenticatedUser();

            var viewModel = new UserInfoViewModel
            {
                booking = booking,
                user = user
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Userinfo(UserInfoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = GetAuthenticatedUser();
                viewModel.booking.UserId = user.Id;
                try
                {
                    _context.Update(viewModel.booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(viewModel.booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ConfirmBooking", new { id = viewModel.booking.Id });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> ConfirmBooking(int id)
        {
            ConfirmBookingViewModel viewModel = GetConfirMBookingViewModel(id);

            return View(viewModel);
        }

        private ConfirmBookingViewModel GetConfirMBookingViewModel(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            var animals = GetAnimalIdsByBooking(booking.Id);
            var user = new User();
            if (User.Identity.IsAuthenticated)
            {
                user = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            }
            var discountController = new DiscountController(animals, user, booking);
            var discounts = discountController.CreateDiscounts();
            var totalDiscounts = discountController.ReturnTotalDiscount();
            if (discounts.Where(d => d.Type == "Eend").Count() > 0)
            {
                booking.chickenDiscount = 2;
                _context.Update(booking);
                _context.SaveChanges();
            }
            else
            {
                booking.chickenDiscount = 1;
                _context.Update(booking);
                _context.SaveChanges();
            }
            var totalAnimalPrice = discountController.returnTotalPrice();
            double nonDiscount = 100 - totalDiscounts;
            var totalPrice = totalAnimalPrice * nonDiscount / 100;

            var viewModel = new ConfirmBookingViewModel()
            {
                animals = animals,
                discounts = discounts,
                totalPrice = totalPrice,
                totalDiscount = totalDiscounts,
                booking = booking
            };
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking(ConfirmBookingViewModel viewModel)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == viewModel.booking.Id);
            booking.IsBevestigd = true;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginRedirect(UserInfoViewModel viewModel)
        {
            var bookingId = viewModel.booking.Id;

            return RedirectToAction("Login", "Account", new { id = bookingId });
        }


        // GET: BookingController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var Booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Booking == null)
            {
                return NotFound();
            }

            return View(Booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(Booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(b => b.Id == id);
        }

        public List<Animal> GetAvailableAnimals()
        {
            List<Animal> availableAnimals = _context.Animals.ToList();
            return availableAnimals;
        }

        public Animal GetAnimalById(int id)
        {
            return _context.Animals.FirstOrDefault(a => a.Id == id);
        }

        private List<int> GetAnimalIdsByDate(DateTime date)
        {
            var bookings = _context.Bookings.ToList().Where(b => b.Date == date);
            var bookingAnimalIds = new List<int>();
            foreach (var booking in bookings) 
            {
                var tempBookingAnimals = _context.bookingAnimals.ToList().Where(b => b.bookingId == booking.Id);
                foreach(var tempAnimal in tempBookingAnimals) { bookingAnimalIds.Add(tempAnimal.animalId); }
            }
            return bookingAnimalIds;
        }

        private Task<User> GetLoggedInUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return _userManager.FindByNameAsync(User.Identity.Name);
            }
            else return null;
        }

        private List<Animal> GetAnimalIdsByBooking(int id)
        {
            var bookingAnimals = _context.bookingAnimals.Where(a => a.bookingId == id);
            var animals = new List<Animal>();
            foreach (var bookAnimal in bookingAnimals)
            {
                animals.Add(_context.Animals.FirstOrDefault(a => a.Id == bookAnimal.animalId));
            }
            return animals;
        }
        private User GetAuthenticatedUser()
        {
            var user = new User();
            if (User.Identity.IsAuthenticated)
            {
                user = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            }

            return user;
        }

    }
}
