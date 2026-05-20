using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.BLL.DTOs
{
    public class CustomerDTO
    {
        public int AccountId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
