using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_WebDev.Data;
using Project_WebDev.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Project_WebDev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;
        private List<Item> _items;

        public HomeController(ILogger<HomeController> logger, ShopContext context)
        {
            _logger = logger;
            _context = context;
            _items = _context.Items.ToList();
        }

        public IActionResult Index()
        {
            return View(_items);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Shop(string order)
        {
            // Sort items based on the 'order' query parameter
            if (order == "asc")
            {
                _items = _items.OrderBy(item => item.Price).ToList();
            }
            else if (order == "desc")
            {
                _items = _items.OrderByDescending(item => item.Price).ToList();
            }

            return View(_items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
