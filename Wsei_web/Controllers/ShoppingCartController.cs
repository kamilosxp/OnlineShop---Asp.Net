using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsei_web.Database;

namespace Wsei_web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShopDbContext _dbContext;
        public ShoppingCartController(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
