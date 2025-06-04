using JobsBookingApp.Repository.Interfaces.Employee;
using JobsBookingApp.Services.DTOs.Employee;
using JobsBookingApp.Services.Interfaces.Employee;

namespace JobsBookingApp.Services.Implementations.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public async Task<GetAllEmployeesResponse> GetAllAsync()
        {
            var employees = await employeeRepository.RetrieveCollectionAsync(new EmployeeFilter()).ToListAsync();

            var allEmployeesResponse = new GetAllEmployeesResponse
            {
                Employees = employees.Select(MapToEmployeeInfo).ToList(),
                TotalCount = employees.Count
            };

            return allEmployeesResponse;
        }

        public async Task<GetEmployeeResponse> GetByIdAsync(int employeeId)
        {
            var employee = await employeeRepository.RetrieveAsync(employeeId);

            return new GetEmployeeResponse
            {
                EmployeeId = employeeId,
                Email = employee.Email,
                Name = employee.Name,
                Username = employee.Username
            };
        }

        private EmployeeInfo MapToEmployeeInfo(Models.Employee employee)
        {
            return new EmployeeInfo
            {
                Email = employee.Email,
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Username = employee.Username
            };
        }
    }
}
