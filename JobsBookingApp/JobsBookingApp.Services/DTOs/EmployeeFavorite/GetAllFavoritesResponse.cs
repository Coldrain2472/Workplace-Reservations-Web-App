using JobsBookingApp.Services.DTOs.Workplace;

namespace JobsBookingApp.Services.DTOs.EmployeeFavorite
{
    public class GetAllFavoritesResponse
    {
        public int EmployeeId { get; set; }

        public List<WorkplaceInfo>? Favorites { get; set; }

        public int TotalCount { get; set; }
    }
}
