using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wsei_web.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public byte[] Image { get; set; }

        public Category Category { get; set; } 
    }
}
