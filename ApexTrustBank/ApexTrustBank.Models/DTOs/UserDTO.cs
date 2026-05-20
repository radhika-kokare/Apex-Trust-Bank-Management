using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.Models.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Otp { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public DateTime? Otpexpiry { get; set; }
        public string? Name { get; set; }
    }
}
