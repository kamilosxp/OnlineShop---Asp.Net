using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wsei_web.Database;
using Wsei_web.Helpers;
using Wsei_web.Models;

namespace Wsei_web.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShopDbContext _dbContext;
        private const string SessionName = "cart";
        public CartController(IHttpContextAccessor httpContextAccessor, ShopDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var cart = _httpContextAccessor.HttpContext?.Session.GetObjectFromJson<List<CartItem>>(SessionName) ?? new List<CartItem>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.UnitPrice * item.Quantity);

            return View();
        }

        [HttpPost]
        public IActionResult Buy([FromBody] Product prod)
        {
            string id = prod.Id.ToString();

            if (_httpContextAccessor.HttpContext == null)
                return BadRequest();

            var product = new Product();
            if (_httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionName) == null)
            {
                var cart = new List<CartItem> { CreateCartItem(int.Parse(id)) };

                _httpContextAccessor.HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                var cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionName);

                if (Exists(int.Parse(id)))
                {
                    var cartItem = cart.FirstOrDefault(p => p.Product.Id == int.Parse(id));
                    if (cartItem != null) cartItem.Quantity++;
                }
                else
                {
                    cart.Add(CreateCartItem(int.Parse(id)));
                }


                _httpContextAccessor.HttpContext.Session.SetObjectAsJson("cart", cart);
            }

            return Ok();
        }

        public IActionResult Remove(string id)
        {
            if (_httpContextAccessor.HttpContext == null) return BadRequest();
            List<CartItem> cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionName);

            var intId = int.Parse(id);
            if (Exists(intId))
            {
                var cartItem = cart.FirstOrDefault(p => p.Id == intId);
                cart.Remove(cartItem);
            }
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson("cart", cart);

            return RedirectToAction("Index");
        }

        private CartItem CreateCartItem(int id)
        {
            var cartItem = new CartItem
            {
                Quantity = 1,
                DateCreated = DateTime.Now,
                Product = GetProduct(id),
                Id = id
            };

            return cartItem;
        }

        private bool Exists(int id)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                List<CartItem> cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionName);
                return cart.Exists(p => p.Id == id);
            }

            return false;
        }

        private Product GetProduct(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

    }
}
