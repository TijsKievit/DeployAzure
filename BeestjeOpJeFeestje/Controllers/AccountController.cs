using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly BeestjeOpJeFeestjeContext _context;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager, BeestjeOpJeFeestjeContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [Authorize(Roles = "Boer")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        [Authorize(Roles = "Boer")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Boer")]
        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [Authorize(Roles = "Boer")]
        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Name))
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
            return View(user);
        }

        [Authorize(Roles = "Boer")]
        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Boer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Boer")]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Boer")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM Input)
        {
            if (ModelState.IsValid)
            {
                var password = GenerateRandomPassword(10);
                var user = new User { UserName = Input.Email, Email = Input.Email , Name = Input.Name, Adres = Input.Adres, PhoneNumber = Input.PhoneNumber};
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    GeneratedPasswordViewModel passwordModel = new GeneratedPasswordViewModel(password, user.Email);
                    _userManager.AddToRoleAsync(user, "Klant").Wait();
                    return View("ShowingPassword", passwordModel);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        [Authorize(Roles = "Boer")]
        public IActionResult ShowingPassword()
        {
            return View();
        }

        public IActionResult Login(int id)
        {
            var ids = id;
            var viewmodel = new LoginVM() { BookingId = id };
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM Input)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
                    if (Input.BookingId != 0)
                    {
                        return RedirectToAction("UserInfo", "Booking",new { id = Input.BookingId });
                    }
                    return LocalRedirect("/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(Input);

                }

            }
            return View(Input);
        }

        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/Account/Login");

        }

        [Authorize(Roles = "Boer")]
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Boer")]
        static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-=_+";
            StringBuilder password = new StringBuilder();

            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[1];

                while (password.Length < length)
                {
                    rngCsp.GetBytes(randomNumber);

                    // Ensure that the random number falls within the valid character range
                    char randomChar = (char)(randomNumber[0] % validChars.Length);

                    password.Append(validChars[randomChar]);
                }
            }

            return password.ToString();
        }

    }
}
