using JobsBookingApp.Repository.Interfaces.Employee;
using JobsBookingApp.Repository.Interfaces.EmployeeFavorite;
using JobsBookingApp.Repository.Interfaces.Workplace;
using JobsBookingApp.Services.DTOs.EmployeeFavorite;
using JobsBookingApp.Services.DTOs.Workplace;
using JobsBookingApp.Services.Interfaces.EmployeeFavorite;

namespace JobsBookingApp.Services.Implementations.EmployeeFavorite
{
    public class EmployeeFavoriteService : IEmployeeFavoriteService
    {
        private readonly IEmployeeFavoriteRepository employeeFavoriteRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWorkplaceRepository workplaceRepository;

        public EmployeeFavoriteService(IEmployeeFavoriteRepository _employeeFavoriteRepository, IEmployeeRepository _employeeRepository, IWorkplaceRepository _workplaceRepository)
        {
            employeeFavoriteRepository = _employeeFavoriteRepository;
            employeeRepository = _employeeRepository;
            workplaceRepository = _workplaceRepository;
        }

        public async Task<AddFavoriteResponse> AddFavoriteAsync(AddFavoriteRequest request)
        {
            var employee = await employeeRepository.RetrieveAsync(request.EmployeeId);
            if (employee == null)
            {
                return new AddFavoriteResponse 
                { 
                    Success = false, 
                    ErrorMessage = "Employee not found." 
                };
            }

            var workplace = await workplaceRepository.RetrieveAsync(request.WorkplaceId);
            if (workplace == null)
            {
                return new AddFavoriteResponse 
                { 
                    Success = false, 
                    ErrorMessage = "Workplace not found." 
                };
            }

            var favorites = await employeeFavoriteRepository
                .RetrieveCollectionAsync(new EmployeeFavoriteFilter { EmployeeId = request.EmployeeId })
                .ToListAsync();

            if (favorites.Any(f => f.WorkplaceId == request.WorkplaceId))
            {
                return new AddFavoriteResponse 
                { 
                    Success = false, 
                    ErrorMessage = "This workplace is already in favorites." 
                };
            }

            if (favorites.Count >= 3)
            {
                return new AddFavoriteResponse 
                { 
                    Success = false, 
                    ErrorMessage = "You can only have up to 3 favorite workplaces." 
                };
            }

            var favorite = new Models.EmployeeFavorite
            {
                EmployeeId = request.EmployeeId,
                WorkplaceId = request.WorkplaceId
            };

            await employeeFavoriteRepository.CreateAsync(favorite);

            return new AddFavoriteResponse 
            { 
                Success = true 
            };
        }

        public async Task<GetAllFavoritesResponse> GetAllFavoritesAsync(GetAllFavoritesRequest request)
        {
            var favorites = await employeeFavoriteRepository
               .RetrieveCollectionAsync(new EmployeeFavoriteFilter { EmployeeId = request.EmployeeId })
               .ToListAsync();

            var response = new GetAllFavoritesResponse
            {
                EmployeeId = request.EmployeeId,
                Favorites = new List<WorkplaceInfo>(),
                TotalCount = favorites.Count
            };

            foreach (var favorite in favorites)
            {
                var workplace = await workplaceRepository.RetrieveAsync(favorite.WorkplaceId);
                if (workplace != null)
                {
                    response.Favorites.Add(new WorkplaceInfo
                    {
                        WorkplaceId = workplace.WorkplaceId,
                        Floor = workplace.Floor,
                        Zone = workplace.Zone,
                        HasMonitor = workplace.HasMonitor,
                        HasDock = workplace.HasDock,
                        IsNearWindow = workplace.IsNearWindow,
                        IsNearPrinter = workplace.IsNearPrinter
                    });
                }
            }

            return response;
        }

        public async Task<RemoveFavoriteResponse> RemoveFavoriteAsync(RemoveFavoriteRequest request)
        {
            var favorites = await employeeFavoriteRepository
                 .RetrieveCollectionAsync(new EmployeeFavoriteFilter
                 {
                     EmployeeId = request.EmployeeId,
                     WorkplaceId = request.WorkplaceId
                 })
                 .ToListAsync();

            var toRemove = favorites.FirstOrDefault();
            if (toRemove == null)
            {
                return new RemoveFavoriteResponse
                {
                    Success = false,
                    ErrorMessage = "Favorite not found."
                };
            }

            await employeeFavoriteRepository.DeleteAsync(toRemove.EmployeeId, toRemove.WorkplaceId);

            return new RemoveFavoriteResponse 
            { 
                Success = true 
            };
        }
    }
}
