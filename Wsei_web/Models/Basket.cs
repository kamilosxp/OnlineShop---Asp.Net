using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wsei_web.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public List<CartItem> ProductsList { get; set; }
    }
}
