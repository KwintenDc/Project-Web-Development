using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Project_WebDev.Models;
using System.Diagnostics;

namespace Project_WebDev.Controllers
{
    public class HomeController : Controller
    {
        List<Item> items = new List<Item>
            {
                new Item { Type = "Assault-Rifle", Name = "AK-12 Mosfet Enchanced", Price = "€483,90", Description = "AK-12 Mosfet Enchanced", State = "Featured", Image = "images/ak12k-mosfet.webp" },
                new Item { Type = "Assault-Rifle", Name = "Custom M4", Price = "€799,90", Description = "Custom M4", State = "Featured", Image = "images/custom-m4.webp" },
                new Item { Type = "Assault-Rifle", Name = "P90 Plus", Price = "€449,90", Description = "P90 Plus", State = "Featured", Image = "images/p90-black.webp" },
                new Item { Type = "Assault-Rifle", Name = "G12V", Price = "€249,90", Description = "G12V", State = "ShopSold", Image = "images/sa-g12v.webp" },
                new Item { Type = "Assault-Rifle", Name = "J08", Price = "€299,90", Description = "J08", State = "ShopSold", Image = "images/sa-j08.webp" },
                new Item { Type = "Assault-Rifle", Name = "PP19", Price = "€369,90", Description = "PP19", State = "ShopSold", Image = "images/pp19-1-mosfet.webp" },

            };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {        
            return View(items);
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

        public IActionResult Shop()
        {
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}