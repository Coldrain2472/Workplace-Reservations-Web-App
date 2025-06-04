using JobsBookingApp.Repository.Interfaces.Workplace;
using JobsBookingApp.Services.DTOs.Workplace;
using JobsBookingApp.Services.Interfaces.Workplace;

namespace JobsBookingApp.Services.Implementations.Workplace
{
    public class WorkplaceService : IWorkplaceService
    {
        private readonly IWorkplaceRepository workplaceRepository;

        public WorkplaceService(IWorkplaceRepository _workplaceRepository)
        {
            workplaceRepository = _workplaceRepository;
        }

        public async Task<GetAllWorkplacesResponse> GetAllAsync()
        {
            var workplaces = await workplaceRepository.RetrieveCollectionAsync(new WorkplaceFilter()).ToListAsync();

            var allWorkplacesResponse = new GetAllWorkplacesResponse
            {
                Workplaces = workplaces.Select(MapToWorkplaceInfo).ToList(),
                TotalCount = workplaces.Count
            };

            return allWorkplacesResponse;
        }

        public async Task<GetWorkplaceResponse> GetByIdAsync(int workplaceId)
        {
           var workplace = await workplaceRepository.RetrieveAsync(workplaceId);

            if (workplace == null)
            {
                throw new InvalidOperationException($"Workplace with ID {workplaceId} not found.");
            }
            return new GetWorkplaceResponse
            {
                IsAvailable = workplace.IsAvailable,
                Floor = workplace.Floor,
                HasDock = workplace.HasDock,
                HasMonitor = workplace.HasMonitor,
                IsNearPrinter = workplace.IsNearPrinter,
                IsNearWindow = workplace.IsNearWindow,
                Zone = workplace.Zone,
                WorkplaceId = workplaceId
            };
        }

        private WorkplaceInfo MapToWorkplaceInfo(Models.Workplace workplace)
        {
            return new WorkplaceInfo
            {
                Floor = workplace.Floor,
                Zone = workplace.Zone,
                HasMonitor = workplace.HasMonitor,
                HasDock = workplace.HasDock,
                IsNearWindow = workplace.IsNearWindow,
                IsNearPrinter = workplace.IsNearPrinter,
                IsAvailable = workplace.IsAvailable,
                WorkplaceId = workplace.WorkplaceId
            };
        }
    }
}
