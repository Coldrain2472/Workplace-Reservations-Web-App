using JobsBookingApp.Repository.Base;

namespace JobsBookingApp.Repository.Interfaces.Workplace
{
    public interface IWorkplaceRepository : IBaseRepository<Models.Workplace, WorkplaceFilter, WorkplaceUpdate>
    {
    }
}
