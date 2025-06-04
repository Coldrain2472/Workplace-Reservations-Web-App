using JobsBookingApp.Services.DTOs.Authentication;

namespace JobsBookingApp.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
