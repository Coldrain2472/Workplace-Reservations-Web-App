using JobsBookingApp.Services.Interfaces.Employee;
using JobsBookingApp.Services.Interfaces.Reservation;
using JobsBookingApp.Web.Attributes;
using JobsBookingApp.Web.Helpers;
using JobsBookingApp.Web.Models.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace JobsBookingApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IEmployeeService employeeService;

        public HomeController(IReservationService _reservationService, IEmployeeService _employeeService)
        {
            reservationService = _reservationService;
            employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employeeId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(employeeId);
            var now = DateTime.Now;
            var reservations = await reservationService.GetAllAsync();
            var inactiveReservations = reservations.Reservations
                .Where(r => r.EmployeeId == employeeId && r.EndTime <= now)
                .Select(r => new HomeReservationViewModel
                {
                    CreatedAt = r.CreatedAt,
                    EmployeeId = r.EmployeeId,
                    EmployeeName = employee.Name,
                    EndTime = r.EndTime,
                    ReservationId = r.ReservationId,
                    StartTime = r.StartTime,
                    WorkplaceId = r.WorkplaceId
                })
                .ToList();

            var activeReservations = reservations.Reservations
                .Where(r => r.EmployeeId == employeeId && r.EndTime > now)
                .Select(r => new HomeReservationViewModel
                {
                    CreatedAt = r.CreatedAt,
                    EmployeeId = r.EmployeeId,
                    EmployeeName = employee.Name,
                    EndTime = r.EndTime,
                    ReservationId = r.ReservationId,
                    StartTime = r.StartTime,
                    WorkplaceId = r.WorkplaceId
                })
                .ToList();

            var viewModel = new HomeReservationListViewModel
            {
                ActiveReservations = activeReservations,
                InactiveReservations = inactiveReservations
            };

            return View(viewModel);
        }
    }
}
