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
            var passwordDb = _context.Customers.FirstOrDefault(c => c.Password == password);

            if ((customer != null) && (passwordDb != null))
            {
                HttpContext.Session.SetString("CurrentCustomer", JsonSerializer.Serialize(customer));

                return RedirectToAction("Shop", "Home");
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
            // TODOKWINTEN : Passwords hashen! 
            if (password != confirmPassword)
            {
                string err = "The password and confirm password do not match.";
                return View("Register", err);
            }

            Customer customer = new ()
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
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }

            if (currentCustomer != null)
            {
                var activeOrder = _context.Orders
                    .Include(o => o.OrderDetails) 
                    .FirstOrDefault(o => o.CustomerId == currentCustomer.Id && o.OrderFullFilled == null);

                if (activeOrder != null)
                {
                    return View(activeOrder.OrderDetails);
                }
                else
                {
                    return View(null);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int itemId)
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
            {
                var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

                if (item != null)
                {
                    var activeOrder = _context.Orders.Include(o => o.OrderDetails)
                                        .FirstOrDefault(o => o.CustomerId == currentCustomer.Id && o.OrderFullFilled == null);

                    if (activeOrder == null)
                    {
                        activeOrder = new Order
                        {
                            OrderPlaced = DateTime.Now,
                            CustomerId = currentCustomer.Id,
                            OrderDetails = new List<OrderDetails>()
                        };

                        _context.Orders.Add(activeOrder);
                        currentCustomer.Orders.Add(activeOrder);
                        _context.SaveChanges();
                    }

                    var existingOrderDetail = activeOrder.OrderDetails.FirstOrDefault(od => od.ProductId == itemId);
                    if (existingOrderDetail != null)
                    {
                        existingOrderDetail.Quantity += 1;
                    }
                    else
                    {
                        var orderDetails = new OrderDetails
                        {
                            Quantity = 1,
                            ProductId = itemId,
                            OrderId = activeOrder.Id,
                            Order = activeOrder,
                            Item = item
                        };

                        activeOrder.OrderDetails.Add(orderDetails);
                    }
                    _context.SaveChanges();

                    return Ok();
                }

                return NotFound();
            }
            else
            {
                return View("Login");
            }
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int orderDetailId, int quantity)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(od => od.Id == orderDetailId);

            if (orderDetail != null)
            {
                orderDetail.Quantity = quantity;
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int orderDetailId)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(od => od.Id == orderDetailId);

            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();

        }
        [HttpPost]
        public IActionResult Checkout()
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
            {
                var activeOrder = _context.Orders.Include(o => o.OrderDetails)
                                       .FirstOrDefault(o => o.CustomerId == currentCustomer.Id && o.OrderFullFilled == null);
                if (activeOrder != null)
                {
                    activeOrder.OrderFullFilled = DateTime.Now;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

        public IActionResult Summary()
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                //.Where(o => o.OrderFullFilled != null)
                .ToList();

            var aggregatedOrderDetails = orders
                .SelectMany(o => o.OrderDetails)
                .GroupBy(od => od.Item.Name)
                .Select(g => new AggregatedOrderDetail
                {
                    ItemName = g.Key,
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalPrice = g.Sum(od => od.Quantity * od.Item.Price)
                })
                .ToList();

            ViewData["AggregatedOrderDetails"] = aggregatedOrderDetails;

            return View(orders);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
