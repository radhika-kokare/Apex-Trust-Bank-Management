using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTrustBank.Models.DTOs
{
        public class ChangePasswordResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; } = string.Empty;
        }
}
