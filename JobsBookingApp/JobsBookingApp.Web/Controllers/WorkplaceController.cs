using JobsBookingApp.Services.DTOs.EmployeeFavorite;
using JobsBookingApp.Services.DTOs.Reservation;
using JobsBookingApp.Services.Interfaces.Employee;
using JobsBookingApp.Services.Interfaces.EmployeeFavorite;
using JobsBookingApp.Services.Interfaces.Reservation;
using JobsBookingApp.Services.Interfaces.Workplace;
using JobsBookingApp.Web.Attributes;
using JobsBookingApp.Web.Helpers;
using JobsBookingApp.Web.Models.Workplace;
using Microsoft.AspNetCore.Mvc;

namespace JobsBookingApp.Web.Controllers
{
    [Authorize]
    public class WorkplaceController : Controller
    {
        private readonly IWorkplaceService workplaceService;
        private readonly IEmployeeFavoriteService employeeFavoriteService;
        private readonly IReservationService reservationService;
        private readonly IEmployeeService employeeService;

        public WorkplaceController(IWorkplaceService _workplaceService, IEmployeeFavoriteService _employeeFavoriteService, IReservationService _reservationService,
            IEmployeeService _employeeService)
        {
            workplaceService = _workplaceService;
            employeeFavoriteService = _employeeFavoriteService;
            reservationService = _reservationService;
            employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employeeId = IdHelper.GetUserId(HttpContext);
            var workplaces = await workplaceService.GetAllAsync();
            var favoritesResponse = await employeeFavoriteService.GetAllFavoritesAsync(new GetAllFavoritesRequest { EmployeeId = employeeId });
            var favoriteWorkplaceIds = favoritesResponse.Favorites?.Select(f => f.WorkplaceId).ToList() ?? new List<int>();

            var sortedWorkplaces = workplaces.Workplaces
                .OrderByDescending(wp => favoriteWorkplaceIds.Contains(wp.WorkplaceId)) // favorites should be up top
                .ToList();

            var viewModel = new WorkplaceListViewModel
            {
                Workplaces = sortedWorkplaces.Select(w => new WorkplaceViewModel
                {
                    WorkplaceId = w.WorkplaceId,
                    Floor = w.Floor,
                    Zone = w.Zone,
                    HasMonitor = w.HasMonitor,
                    HasDock = w.HasDock,
                    IsNearWindow = w.IsNearWindow,
                    IsNearPrinter = w.IsNearPrinter,
                    IsAvailable = w.IsAvailable,
                    IsFavorite = favoriteWorkplaceIds.Contains(w.WorkplaceId)
                })
                .ToList(),
                TotalCount = workplaces.TotalCount
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(int workplaceId)
        {
            int employeeId = IdHelper.GetUserId(HttpContext);

            var response = await employeeFavoriteService.AddFavoriteAsync(new AddFavoriteRequest
            {
                EmployeeId = employeeId,
                WorkplaceId = workplaceId
            });

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int workplaceId)
        {
            int employeeId = IdHelper.GetUserId(HttpContext);

            var response = await employeeFavoriteService.RemoveFavoriteAsync(new RemoveFavoriteRequest
            {
                EmployeeId = employeeId,
                WorkplaceId = workplaceId
            });

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> QuickReserve(QuickReserveViewModel model)
        {
            if (model.WorkplaceId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid workplace ID.";
                return RedirectToAction("Index", "Workplace");
            }

            var currentEmployeeId = IdHelper.GetUserId(HttpContext);

            var favoritesRequest = new GetAllFavoritesRequest { EmployeeId = currentEmployeeId };
            var favorites = await employeeFavoriteService.GetAllFavoritesAsync(favoritesRequest);
            bool isFavorite = favorites.Favorites.Any(f => f.WorkplaceId == model.WorkplaceId);

            if (!isFavorite)
            {
                TempData["ErrorMessage"] = "You can only quick reserve your favorite workplaces.";
                return RedirectToAction("Index", "Workplace");
            }

            var nextWorkDay = DateHelper.GetNextWorkingDay();

            var request = new CreateReservationRequest
            {
                EmployeeId = currentEmployeeId,
                WorkplaceId = model.WorkplaceId,
                StartTime = nextWorkDay
            };

            var result = await reservationService.CreateQuickReservationAsync(request);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction("Index", "Workplace");
            }

            TempData["SuccessMessage"] = "Quick reservation created successfully.";
            return RedirectToAction("Index", "Reservation");
        }

    }
}
