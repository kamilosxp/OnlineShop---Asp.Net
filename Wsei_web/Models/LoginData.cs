using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wsei_web.Models
{
    public class LoginData
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public User User { get; set; }
    }
}
