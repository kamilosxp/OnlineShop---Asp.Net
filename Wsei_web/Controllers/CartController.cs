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
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShopDbContext _dbContext;
        private const string _sessionName = "cart";
        public CartController(IHttpContextAccessor httpContextAccessor, ShopDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        [Route("index")]
        public IActionResult Index()
        {
            var cart = _httpContextAccessor.HttpContext?.Session.GetObjectFromJson<List<CartItem>>(_sessionName);
            if (cart != null)
            {
                var totalCost = cart.Sum(item => item.Product.UnitPrice * item.Quantity);
            }

            return null;
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            if (_httpContextAccessor.HttpContext == null)
                return BadRequest();

            var product = new Product();
            if (_httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(_sessionName) == null)
            {
                var cart = new List<CartItem> {CreateCartItem(int.Parse(id))};

                _httpContextAccessor.HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                var cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(_sessionName);

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

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                List<CartItem> cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(_sessionName);
                if (Exists(int.Parse(id)))
                {
                    var cartItem = cart.FirstOrDefault(p => p.Id == id);
                    cart.Remove(cartItem);
                }
                _httpContextAccessor.HttpContext.Session.SetObjectAsJson("cart", cart);
                return Ok();
            }

            return BadRequest();
        }

        private CartItem CreateCartItem(int id)
        {
            var cartItem = new CartItem
            {
                Quantity = 1,
                DateCreated = DateTime.Now,
                Product = _dbContext.Products.FirstOrDefault(p => p.Id == id)
            };

            return cartItem;
        }

        private bool Exists(int id)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                List<CartItem> cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(_sessionName);
                return cart.Exists(p => int.Parse(p.Id) == id);
            }

            return false;
        }
    }
}
