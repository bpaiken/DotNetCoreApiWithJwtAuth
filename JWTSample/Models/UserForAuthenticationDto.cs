using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSample.Models
{
    public class UserForAuthenticationDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
