using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wsei_web.Database;

namespace Wsei_web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopDbContext _dbContext;
        public ProductsController(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(m => m.Category).ToList();
            return View("Products", products);
        }
    }
}
