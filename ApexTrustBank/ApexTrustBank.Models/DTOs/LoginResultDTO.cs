using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.Models.DTOs
{
    public class LoginResultDTO
    {
        public int UserId { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string? Token { get; set; }

    }

}
