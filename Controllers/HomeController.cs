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
                new Item { Type = "AEG-Assault-Rifle", Name = "AK-12 Mosfet Enchanced", Price = "€483,90", Description = "AK-12 Mosfet Enchanced", State = "Featured", Image = "images/ak12k-mosfet.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "Custom M4", Price = "€799,90", Description = "Custom M4", State = "Featured", Image = "images/custom-m4.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "P90 Plus", Price = "€449,90", Description = "P90 Plus", State = "Featured", Image = "images/p90-black.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "G12V", Price = "€249,90", Description = "G12V", State = "ShopSold", Image = "images/sa-g12v.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "J08", Price = "€299,90", Description = "J08", State = "ShopSold", Image = "images/sa-j08.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "PP19", Price = "€369,90", Description = "PP19", State = "ShopSold", Image = "images/pp19-1-mosfet.webp" },

                new Item { Type = "Gas-Pistol", Name = "P09", Price = "€159,99", Description = "P09", State = "ShopSold", Image = "images/p-09-gbb-tan.webp" },

                new Item { Type = "HPA-Gun", Name = "Article 1", Price = "€967,90", Description = "Article 1", State = "ShopSold", Image = "images/article-1-heretic-labs.webp" },

                new Item { Type = "Sniper", Name = "SSG96 mk2", Price = "€399,90", Description = "SSG96 mk2", State = "ShopSold", Image = "images/ssg96-mk2.webp" },

                new Item { Type = "Shotgun", Name = "M56b Short", Price = "€59,90", Description = "M56b Short", State = "ShopSold", Image = "images/m56b-shorty.webp" },
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