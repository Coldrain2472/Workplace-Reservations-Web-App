using JobsBookingApp.Services.DTOs.Reservation;
using JobsBookingApp.Services.Interfaces.Employee;
using JobsBookingApp.Services.Interfaces.EmployeeFavorite;
using JobsBookingApp.Services.Interfaces.Reservation;
using JobsBookingApp.Services.Interfaces.Workplace;
using JobsBookingApp.Web.Attributes;
using JobsBookingApp.Web.Helpers;
using JobsBookingApp.Web.Models.Reservation;
using JobsBookingApp.Web.Models.Workplace;
using Microsoft.AspNetCore.Mvc;

namespace JobsBookingApp.Web.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IEmployeeService employeeService;
        private readonly IEmployeeFavoriteService employeeFavoriteService;
        private readonly IWorkplaceService workplaceService;

        public ReservationController(IReservationService _reservationService, IEmployeeFavoriteService _employeeFavoriteService,
            IEmployeeService _employeeService, IWorkplaceService _workplaceService)
        {
            reservationService = _reservationService;
            employeeService = _employeeService;
            employeeFavoriteService = _employeeFavoriteService;
            workplaceService = _workplaceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reservations = await reservationService.GetAllAsync();
            var viewModel = new ReservationListViewModel
            {
                Reservations = reservations.Reservations.Select(r => new ReservationViewModel
                {
                    ReservationId = r.ReservationId,
                    EmployeeId = r.EmployeeId,
                    WorkplaceId = r.WorkplaceId,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    CreatedAt = r.CreatedAt,
                    EmployeeName = employeeService.GetByIdAsync(r.EmployeeId).Result?.Name ?? "Unknown"
                })
                .ToList(),
                TotalCount = reservations.TotalCount
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var currentEmployeeId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentEmployeeId);
            var workplaces = await workplaceService.GetAllAsync();

            var viewModel = new ReservationCreateViewModel
            {
                Workplaces = workplaces.Workplaces.Select(w => new WorkplaceViewModel
                {
                    WorkplaceId = w.WorkplaceId,
                    Floor = w.Floor,
                    IsNearWindow = w.IsNearWindow,
                    IsAvailable = w.IsAvailable,
                    HasDock = w.HasDock,
                    HasMonitor = w.HasMonitor,
                    Zone = w.Zone,
                    IsNearPrinter = w.IsNearPrinter
                }).ToList(),
                EmployeeId = currentEmployeeId,
                EmployeeName = employee?.Name ?? "Unknown",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(8),
                CreatedAt = DateTime.Now
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateViewModel model)
        {
            var currentEmployeeId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentEmployeeId);
            var request = new CreateReservationRequest
            {
                EmployeeId = currentEmployeeId,
                WorkplaceId = model.WorkplaceId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                CreatedAt = model.CreatedAt,
                EmployeeName = employee.Name,
                ReservationId = model.ReservationId
            };

            var response = await reservationService.CreateReservationAsync(request);

            if (!response.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

      
    }
}
