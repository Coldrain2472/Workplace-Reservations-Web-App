namespace JobsBookingApp.Services.DTOs.Reservation
{
    public class ReservationInfo
    {
        public int ReservationId { get; set; }

        public int EmployeeId { get; set; }

        public int WorkplaceId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
