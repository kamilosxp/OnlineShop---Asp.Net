using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wsei_web.Database;
using Wsei_web.Models;

namespace Wsei_web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShopDbContext _dbContext;
        private readonly ILogger<ProductsController> _logger;

        public AdminController(ShopDbContext dbContext, ILogger<ProductsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(m => m.Category).ToList();
            return View("Products", products);
        }

        public IActionResult AddProduct()
        {
            return View("AddProduct");
        }

        public IActionResult Products()
        {
            var products = _dbContext.Products.Include(m => m.Category).ToList();
            return View(products);
        }
        public IActionResult Orders()
        {
            var orders = _dbContext.Orders
                .Include(x => x.OrderAddress)
                .ToList();
            return View(orders);
        }

        public IActionResult Users()
        {
            var users = _dbContext.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid data.");
                return BadRequest();
            }

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Ok();
        }

        public ActionResult Delete(int? id)
        {
            var products = _dbContext.Products.ToList();
            var product = products.FirstOrDefault(a => a.Id == id);
            if (product == null)
            {
                _logger.LogError("Not a valid product id.");
                return BadRequest("Not a valid product id.");
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return RedirectToAction("Products", products);
        }
    }
}
