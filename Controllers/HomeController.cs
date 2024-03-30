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
using System.Security.Cryptography;
using System.Text;


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
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if(currentCustomer != null)
                ViewBag.Customer = currentCustomer;
            return View(_items);
        }

        public IActionResult About()
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;
            return View();
        }

        public IActionResult Contact()
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;

            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);

            if (customer != null)
            {
                // Verify the hashed password
                byte[] hashBytes = Convert.FromBase64String(customer.Password);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);

                // Compare the hashed password
                bool passwordVerified = true;
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        passwordVerified = false;
                        break;
                    }
                }

                if (passwordVerified)
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
            else
            {
                string err = "Incorrect email or password";
                return View("Login", err);
            }
        }

        public IActionResult Login()
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;

            return View();
        }

        [HttpPost]
        public IActionResult Register(string firstname, string lastname, string email, string number, string password, string confirmPassword)
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;

            // TODOKWINTEN : Passwords hashen! 
            if (password != confirmPassword)
            {
                string err = "The password and confirm password do not match.";
                return View("Register", err);
            }

            // Hash the password before saving
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            Customer customer = new()
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Phone = number,
                Password = hashedPassword // Save hashed password
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Register() 
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
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
                if(currentCustomer.Role == "Pending")
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Customer = currentCustomer;
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
                ViewBag.Customer = currentCustomer;

            if (currentCustomer != null && currentCustomer.Role != "Pending")
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
                ViewBag.Customer = currentCustomer;
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
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;
            if (currentCustomer != null && (currentCustomer.Role == "Admin" || currentCustomer.Role == "Employee"))
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
            else
                return View("Login");
        }
        
        public IActionResult Verify()
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;

            if (currentCustomer != null && currentCustomer.Role == "Admin")
            {
                var customers = _context.Customers.ToList();
                return View(customers);
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult ChangeRole(int userId, string action)
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
            {
                
                var user = _context.Customers.FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    return NotFound();
                }

                if (action == "promote")
                {
                    if (user.Role == "Pending")
                        user.Role = "User";
                    else if (user.Role == "User")
                        user.Role = "Employee";
                    else if (user.Role == "Employee")
                        user.Role = "Admin";
                }
                else if (action == "demote")
                {
                    if (user.Role == "User")
                        _context.Customers.Remove(user);
                    else if (user.Role == "Employee")
                        user.Role = "User";
                    else if (user.Role == "Admin")
                        if (currentCustomer.Id != user.Id)
                            user.Role = "Employee";
                        else
                            return RedirectToAction("Verify");
                }

                _context.SaveChanges();
                return RedirectToAction("Verify");
            }
            else
                return RedirectToAction("Index");
            
        }
        public IActionResult OrderAdmin()
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;

            if (currentCustomer != null && currentCustomer.Role == "Admin")
            {
                var orders = _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    //.Where(o => o.OrderFullFilled != null)
                    .ToList();
                return View(orders);
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult UpdateQuantityAdmin(int orderDetailId, int quantity)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(od => od.Id == orderDetailId);

            if (orderDetail == null)
            {
                return NotFound();
            }

            orderDetail.Quantity = quantity;

            _context.SaveChanges();

            double totalPrice = orderDetail.Quantity * orderDetail.Item.Price;

            return Json(new { totalPrice });
        }

        [HttpPost]
        public IActionResult DeleteSelectedOrders(List<int> selectedOrderIds)
        {

            foreach (int orderId in selectedOrderIds)
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    _context.OrderDetails.RemoveRange(order.OrderDetails);
                    _context.SaveChanges();
                }
            }
            return Ok(new { message = "Selected orders deleted successfully" });
        }

        public IActionResult ProductsAdmin()
        {
            var currentCustomerJson = HttpContext.Session.GetString("CurrentCustomer");
            if (!string.IsNullOrEmpty(currentCustomerJson))
            {
                currentCustomer = JsonSerializer.Deserialize<Customer>(currentCustomerJson);
            }
            if (currentCustomer != null)
                ViewBag.Customer = currentCustomer;

            if (currentCustomer != null && currentCustomer.Role == "Admin")
            {
                var items = _context.Items
                    .ToList();
                return View(items);
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult UpdateItemPrice(int itemId, string newPrice)
        {
            // Find the item by its ID
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                return NotFound(); 
            }

            item.Price = Convert.ToDouble(newPrice.Replace(".",","));

            _context.SaveChanges();

            return Ok(); 
        }

        // TODOKWINTEN : Passwords hashen in de database (link is opgeslaan).
        // TODOKWINTEN : Purge van alle orders om 14u30 (indien de site actief is) -> OPT


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
