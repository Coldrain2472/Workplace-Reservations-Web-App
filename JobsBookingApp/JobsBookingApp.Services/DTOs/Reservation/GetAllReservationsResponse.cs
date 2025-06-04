namespace JobsBookingApp.Services.DTOs.Reservation
{
    public class GetAllReservationsResponse
    {
        public List<ReservationInfo>? Reservations { get; set; }

        public int TotalCount { get; set; }
    }
}
