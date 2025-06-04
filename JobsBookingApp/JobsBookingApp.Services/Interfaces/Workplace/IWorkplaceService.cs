using JobsBookingApp.Services.DTOs.Workplace;

namespace JobsBookingApp.Services.Interfaces.Workplace
{
    public interface IWorkplaceService
    {
        Task<GetWorkplaceResponse> GetByIdAsync(int workplaceId);

        Task<GetAllWorkplacesResponse> GetAllAsync();
    }
}
