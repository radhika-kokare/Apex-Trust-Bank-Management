using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.Models.Response
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Token { get; set; }

        public DateTime TokenExpiry { get; set; }

        public string Message { get; set; } = "Login successful";

        public bool IsSuccess { get; set; } = true;
    }

}

