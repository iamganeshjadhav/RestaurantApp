using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Models;
using System.Linq;

namespace RestaurantApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user != null)
            {
               
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("User", user.Username);

                return RedirectToAction("Index", "Menu");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}