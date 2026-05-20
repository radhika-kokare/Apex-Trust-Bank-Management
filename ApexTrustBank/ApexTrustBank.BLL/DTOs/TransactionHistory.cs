using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.BLL.DTOs
{
    public class TransactionHistoryDTO
    {
        public int TransactionId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public string Remarks { get; set; } = null!;
    }
}
