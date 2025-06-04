using JobsBookingApp.Services.DTOs.Employee;

namespace JobsBookingApp.Services.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<GetEmployeeResponse> GetByIdAsync(int employeeId);

        Task<GetAllEmployeesResponse> GetAllAsync();
    }
}
