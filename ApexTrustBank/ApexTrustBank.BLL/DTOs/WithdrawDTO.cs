using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.BLL.DTOs
{
    public class WithdrawDTO
    {
        public string AccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
