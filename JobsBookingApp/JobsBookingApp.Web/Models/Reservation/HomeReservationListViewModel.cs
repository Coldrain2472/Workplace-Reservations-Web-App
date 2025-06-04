using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Web.Models.Reservation
{
    public class HomeReservationListViewModel
    {
        public List<HomeReservationViewModel> ActiveReservations { get; set; } = new List<HomeReservationViewModel>();

        public List<HomeReservationViewModel> InactiveReservations { get; set; } = new List<HomeReservationViewModel>();
    }

    public class HomeReservationViewModel
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
