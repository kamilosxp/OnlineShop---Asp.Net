using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wsei_web.Database;
using Wsei_web.Helpers;
using Wsei_web.Models;

namespace Wsei_web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShopDbContext _dbContext;

        public OrderController(IHttpContextAccessor httpContextAccessor, ShopDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.order  = _httpContextAccessor.HttpContext?.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNewOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            order.ItemsJson = _httpContextAccessor.HttpContext?.Session.GetString("cart");
            order.OrderDate = DateTime.Now;
            order.OrderNumber = Guid.NewGuid().ToString();

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return View("Index");
        }
    }
}
