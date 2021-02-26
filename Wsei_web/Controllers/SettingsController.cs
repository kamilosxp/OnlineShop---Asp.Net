using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wsei_web.Database;
using Wsei_web.Models;

namespace Wsei_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ShopDbContext _dbContext;

        public SettingsController(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("AddNewProduct")]
        public ActionResult AddNewProduct(Product product)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest("Invalid data.");
            }

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
