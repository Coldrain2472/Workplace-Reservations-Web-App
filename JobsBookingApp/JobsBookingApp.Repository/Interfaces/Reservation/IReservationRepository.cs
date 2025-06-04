using JobsBookingApp.Repository.Base;

namespace JobsBookingApp.Repository.Interfaces.Reservation
{
    public interface IReservationRepository : IBaseRepository<Models.Reservation, ReservationFilter, ReservationUpdate>
    {
    }
}
