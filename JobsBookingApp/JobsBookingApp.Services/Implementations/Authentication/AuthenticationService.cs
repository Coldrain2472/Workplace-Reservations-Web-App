using JobsBookingApp.Repository.Interfaces.Employee;
using JobsBookingApp.Services.DTOs.Authentication;
using JobsBookingApp.Services.Helpers;
using JobsBookingApp.Services.Interfaces.Authentication;
using System.Data.SqlTypes;

namespace JobsBookingApp.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository employeeRepository;

        public AuthenticationService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Username and password are required."
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new EmployeeFilter { Username = new SqlString(request.Username) };

            var employees = await employeeRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var employee = employees.SingleOrDefault();

            if (employee == null || employee.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password."
                };
            }

            return new LoginResponse
            {
                Success = true,
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                Username = employee.Username
            };
        }
    }
}
