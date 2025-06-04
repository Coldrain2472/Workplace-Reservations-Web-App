namespace JobsBookingApp.Web.Models.Workplace
{
    public class WorkplaceListViewModel
    {
        public List<WorkplaceViewModel> Workplaces { get; set; }

        public int TotalCount { get; set; }
    }

    public class WorkplaceViewModel
    {
        public int WorkplaceId { get; set; }

        public int Floor { get; set; }

        public string Zone { get; set; }

        public bool HasMonitor { get; set; }

        public bool HasDock { get; set; }

        public bool IsNearWindow { get; set; }

        public bool IsNearPrinter { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsFavorite { get; set; }
    }
}
