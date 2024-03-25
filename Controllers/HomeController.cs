using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_WebDev.Data;
using Project_WebDev.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text.Json;

namespace Project_WebDev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;
        private List<Item> _items;
        private Customer currentCustomer;

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
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);

            if (customer != null)
            {
                HttpContext.Session.SetString("CurrentCustomer", JsonSerializer.Serialize(customer));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                string err = "Incorrect email or password";
                return View("Login", err);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string firstname, string lastname, string email, string number, string password, string confirmPassword)
        {
            // Check if passwords match
            if (password != confirmPassword)
            {
                string err = "The password and confirm password do not match.";
                return View("Register", err);
            }

            // Create the customer object
            Customer customer = new Customer()
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Phone = number,
                Password = password 
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Register() 
        { 
            return View();
        }

        public IActionResult Shop(string order)
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null) 
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
            return View("Login");
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
