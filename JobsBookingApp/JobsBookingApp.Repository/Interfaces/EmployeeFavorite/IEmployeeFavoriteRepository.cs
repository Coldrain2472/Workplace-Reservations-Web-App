using JobsBookingApp.Repository.Base;

namespace JobsBookingApp.Repository.Interfaces.EmployeeFavorite
{
    public interface IEmployeeFavoriteRepository : IBaseRepository<Models.EmployeeFavorite, EmployeeFavoriteFilter, EmployeeFavoriteUpdate>
    {
        Task<bool> DeleteAsync(int employeeId, int workplaceId);
    }
}
