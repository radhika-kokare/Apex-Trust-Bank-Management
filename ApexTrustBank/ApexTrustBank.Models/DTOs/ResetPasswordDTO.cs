using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexTrustBank.Models.DTOs
{
        public class ResetPasswordDTO
        {
            [JsonPropertyName("email")]
            public string? Email { get; set; } = string.Empty;

            [JsonPropertyName("otp")]
            public string? Otp { get; set; }

            [JsonPropertyName("newPassword")]
            public string? NewPassword { get; set; } = string.Empty;
        }
    
}
