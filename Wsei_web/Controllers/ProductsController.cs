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
    public class ProductsController : Controller
    {
        private readonly ShopDbContext _dbContext;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(ShopDbContext dbContext, ILogger<ProductsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(m => m.Category).ToList();
            return View("Products", products);
        }
        public IActionResult Category(string category)
        {
            var products = _dbContext.Products
                .Include(m => m.Category)
                .Where(a => a.Category.Name.Equals(category)).ToList();
            return View("Products", products);
        }

        public IActionResult Search(string name)
        {
            ///TODO
            return View("Products");
        }
    }
}
