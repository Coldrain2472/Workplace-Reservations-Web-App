using JobsBookingApp.Repository.Base;

namespace JobsBookingApp.Repository.Interfaces.Employee
{
    public interface IEmployeeRepository : IBaseRepository<Models.Employee, EmployeeFilter, EmployeeUpdate>
    {
    }
}
