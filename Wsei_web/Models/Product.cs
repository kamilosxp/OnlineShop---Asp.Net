using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wsei_web.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        [MaxLength(30)]
        public string Description { get; set; }
        [DisplayName("PLN Price")]
        public float UnitPrice { get; set; }
        [DisplayName("Available")]
        public int Quantity { get; set; }
        public byte[] Image { get; set; }

        public Category Category { get; set; } 
    }
}
