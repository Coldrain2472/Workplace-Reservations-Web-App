using JobsBookingApp.Web.Models.Workplace;
using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Web.Models.Reservation
{
    public class ReservationCreateViewModel
    {
        [Required]
        public int WorkplaceId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        public List<WorkplaceViewModel> Workplaces { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int ReservationId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } 

    }
}
