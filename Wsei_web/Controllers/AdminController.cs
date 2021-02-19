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


        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid data.");
                return BadRequest("Invalid data.");
            }

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
