using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RestaurantApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

    
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var items = _context.Items.ToList();

            var cart = _context.CartItems
                .Include(x => x.Item)
                .Where(x => userId != null && x.UserId == userId)
                .ToList();

            ViewBag.Items = items;
            ViewBag.Cart = cart;
            ViewBag.Username = HttpContext.Session.GetString("User");

            ViewBag.Total = cart.Sum(x => x.Item.Price * x.Quantity);

            return View();
        }

       
        [HttpPost]
        public IActionResult AddToCart(int id, int qty)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var item = _context.Items.Find(id);

            if (item == null)
                return NotFound();

            var existing = _context.CartItems
                .FirstOrDefault(x => x.ItemId == id && x.UserId == userId);

            if (existing != null)
            {
                existing.Quantity += qty;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ItemId = id,
                    Quantity = qty,
                    UserId = userId.Value
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public IActionResult ClearCart()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var userCart = _context.CartItems
                    .Where(x => x.UserId == userId);

                _context.CartItems.RemoveRange(userCart);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}