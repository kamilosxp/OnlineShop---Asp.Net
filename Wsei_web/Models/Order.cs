using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wsei_web.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ItemsJson { get; set; }
        [DisplayName("Order number")]
        public string OrderNumber { get; set; }
        public OrderAddress OrderAddress { get; set; }
    }
}
