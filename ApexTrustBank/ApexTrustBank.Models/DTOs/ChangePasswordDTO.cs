using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexTrustBank.Models.DTOs
{
    public class ChangePasswordDTO
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; } = string.Empty;

        [JsonPropertyName("currentPassword")]
        public string? CurrentPassword { get; set; } = string.Empty;

        [JsonPropertyName("newPassword")]
        public string? NewPassword { get; set; } = string.Empty;

        [JsonPropertyName("confirmNewPassword")]
        public string? ConfirmNewPassword { get; set; } = string.Empty;

    }
}
