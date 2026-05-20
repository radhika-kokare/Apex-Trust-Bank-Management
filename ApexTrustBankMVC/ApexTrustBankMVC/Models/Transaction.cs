using System;
using System.Collections.Generic;

namespace ApexTrustBankMVC.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public string? Remarks { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
