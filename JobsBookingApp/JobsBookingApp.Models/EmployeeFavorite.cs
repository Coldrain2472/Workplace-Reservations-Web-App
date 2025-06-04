using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Models
{
    public class EmployeeFavorite
    {
        [Required(ErrorMessage = "Employee is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Workplace is required.")]
        public int WorkplaceId { get; set; }
    }
}
