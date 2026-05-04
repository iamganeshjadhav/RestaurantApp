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
            var items = _context.Items.ToList();
            var cart = _context.CartItems
             .Include(x => x.Item)
               .ToList();

            ViewBag.Items = items;
            ViewBag.Cart = cart;

            ViewBag.Total = cart.Sum(x => x.Item.Price * x.Quantity);

            return View();
        }

       
        [HttpPost]
        public IActionResult AddToCart(int id, int qty)
        {
            var item = _context.Items.Find(id);

            if (item == null)
                return NotFound();

            var existing = _context.CartItems.FirstOrDefault(x => x.ItemId == id);

            if (existing != null)
            {
                existing.Quantity += qty;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ItemId = id,
                    Quantity = qty
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

       
        public IActionResult ClearCart()
        {
            _context.CartItems.RemoveRange(_context.CartItems);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}