using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.BLL.DTOs
{
    public class TransferDTO
    {
        public string FromAccountNumber { get; set; } = null!;
        public string ToAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
    }    
}
