using System.Data.SqlTypes;

namespace JobsBookingApp.Repository.Interfaces.Reservation
{
    public class ReservationUpdate
    {
        public SqlInt32? WorkplaceId { get; set; }

        public SqlDateTime? EndTime { get; set; }
    }
}
