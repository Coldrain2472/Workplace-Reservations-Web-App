using JobsBookingApp.Services.DTOs.Reservation;

namespace JobsBookingApp.Services.Interfaces.Reservation
{
    public interface IReservationService
    {
        Task<GetReservationResponse> GetByIdAsync(int reservationId);

        Task<GetAllReservationsResponse> GetAllAsync();

        Task<CreateReservationResponse> CreateReservationAsync(CreateReservationRequest request);

        Task<CreateReservationResponse> CreateQuickReservationAsync(CreateReservationRequest request); // for the fav workplace of the employee

    }
}
