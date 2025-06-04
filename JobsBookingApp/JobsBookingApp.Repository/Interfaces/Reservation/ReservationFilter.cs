using System.Data.SqlTypes;

namespace JobsBookingApp.Repository.Interfaces.Reservation
{
    public class ReservationFilter
    {
        public SqlInt32? EmployeeId { get; set; }

        public SqlDateTime? StartTime { get; set; }

        public SqlDateTime? EndTime { get; set; }

        public SqlInt32? WorkplaceId { get; set; }
    }
}
