using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wsei_web.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public Order Order { get; set; }
    }
}
