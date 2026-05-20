using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.Models.DTOs
{
  public class AccountDTO
    {
        public int AccountId { get; set; }

        public string AccountType { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;

        public decimal Balance { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Mobile { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
    