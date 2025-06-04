namespace JobsBookingApp.Services.DTOs.Workplace
{
    public class GetAllWorkplacesResponse
    {
        public List<WorkplaceInfo>? Workplaces { get; set; }

        public int TotalCount { get; set; }
    }
}
