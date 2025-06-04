using JobsBookingApp.Services.Interfaces.Employee;
using JobsBookingApp.Web.Attributes;
using JobsBookingApp.Web.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace JobsBookingApp.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await employeeService.GetAllAsync();
            var viewModel = new EmployeeListViewModel
            {
                Employees = employees.Employees.Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    Email = e.Email,
                    Name = e.Name,
                    Username = e.Username
                })
                .ToList(),
                TotalCount = employees.TotalCount
            };

            return View(viewModel);
        }
    }
}
