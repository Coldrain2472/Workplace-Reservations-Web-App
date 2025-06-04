using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Web.Models.Reservation
{
    public class ReservationListViewModel
    {
        public List<ReservationViewModel> Reservations { get; set; } = new List<ReservationViewModel>();

        public int TotalCount { get; set; }
    }

    public class ReservationViewModel
    {
        public int ReservationId { get; set; }

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Start Time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End Time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        public int WorkplaceId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public string? EmployeeName { get; set; }
    }
}
