using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BeestjeOpJeFeestje.Controllers
{
    [Authorize(Roles = "Boer")]
    public class AnimalController : Controller
    {
        private readonly BeestjeOpJeFeestjeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AnimalController(BeestjeOpJeFeestjeContext context, IWebHostEnvironment webHostEnvironment) 
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: AnimalController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Animals.ToListAsync());
        }

        // GET: AnimalController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var animal = await _context.Animals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            var bookings = new List<Booking>();
            var bookingAnimals = _context.bookingAnimals.Where(b => b.animalId == animal.Id);
            foreach(var bookingAnimal in bookingAnimals)
            {
                bookings.Add(_context.Bookings.FirstOrDefault(b => b.Id == bookingAnimal.bookingId));
            }
            var viewModel = new AnimalDetailsViewModel()
            {
                animal = animal,
                bookings = bookings
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnimalToBooking(AnimalDetailsViewModel viewModel)
        {
            var bookingId = viewModel.selectedBookingId;

            return RedirectToAction("Details", "Booking", new { id = bookingId });
        }

        // GET: AnimalController/Create
        public IActionResult Create()
        {
            CreateAnimalViewModel viewModel = new CreateAnimalViewModel(new Animal(), GetImageNames());
            return View(viewModel);
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: AnimalController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            CreateAnimalViewModel viewModel = new CreateAnimalViewModel(animal, GetImageNames());
            return View(viewModel);
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: AnimalController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _context.Animals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }

        public List<string> GetImageNames()
        {
            var imagesFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            if (Directory.Exists(imagesFolderPath))
            {
                var imageFiles = Directory.GetFiles(imagesFolderPath, "*.jpg"); // Adjust the file extension as needed
                return imageFiles.Select(Path.GetFileName).ToList();
            }
            List<string> empty = new List<string>();
            return empty;
        }
    }
}
