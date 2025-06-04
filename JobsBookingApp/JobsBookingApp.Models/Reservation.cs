using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Start Time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End Time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Workplace is required.")]
        public int WorkplaceId { get; set; }

        [Required(ErrorMessage = "Creation time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
