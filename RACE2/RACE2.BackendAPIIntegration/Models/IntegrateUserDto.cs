using System.ComponentModel.DataAnnotations;

namespace RACE2.BackendAPIIntegration.Models
{
    public class IntegrateUserDto
    {
        public int fronrEndUserId { get; set; }
        [Required, MaxLength(64)]
        public string backEndUserId { get; set; } = string.Empty;
        [Required]
        public bool isEngineer { get; set; }
        [Required]
        public bool isUndertaker { get; set; }
        [Required]
        public bool isUndertakerContact { get; set; }
        [Required]
        public bool isPrimaryContact { get; set; }
        [MaxLength(128)]
        public string displayName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string title { get; set; } = string.Empty;
        [Required, MaxLength(64)]
        public string firstName { get; set; } = string.Empty;
        [Required]
        public string lastName { get; set; } = string.Empty;
        [MaxLength(80)]
        public string jobTitle { get; set; } = string.Empty;
        [Required, MaxLength(84)]
        public string email { get; set; } = string.Empty;
        [MaxLength(84)]
        public string alternativeEmail { get; set; } = string.Empty;
        [MaxLength(64)]
        public string mobile { get; set; } = string.Empty;
        [MaxLength(64)]
        public string alternativeMobile { get; set; } = string.Empty;
        [MaxLength(64)]
        public string phone { get; set; } = string.Empty;
        [MaxLength(64)]
        public string alternativePhone { get; set; } = string.Empty;
        [MaxLength(64)]
        public string emergencyPhone { get; set; } = string.Empty;
        [MaxLength(64)]
        public string alternativeEmergencyPhone { get; set; } = string.Empty;
        [MaxLength(64)]
        public string paon { get; set; } = string.Empty;
        [MaxLength(64)]
        public string secondaryPaon { get; set; } = string.Empty;
        [MaxLength(64)]
        public string saon { get; set; } = string.Empty;
        [MaxLength(64)]
        public string secondarySaon { get; set; } = string.Empty;

        [MaxLength(64)]
        public string status { get; set; } = string.Empty;
    }
}
