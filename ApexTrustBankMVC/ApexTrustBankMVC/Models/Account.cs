using System;
using System.Collections.Generic;

namespace ApexTrustBankMVC.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int? AccountId { get; set; }
        public string AccountType { get; set; } = null!;
        public decimal? Balance { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? AccountNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
