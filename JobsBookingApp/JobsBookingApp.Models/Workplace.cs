using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Models
{
    public class Workplace
    {
        public int WorkplaceId { get; set; }

        [Required(ErrorMessage = "Floor is required.")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Zone is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Zone must be between 1 and 50 characters.")]
        public string Zone { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool HasMonitor { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool HasDock { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool IsNearWindow { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool IsNearPrinter { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
