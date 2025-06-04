namespace JobsBookingApp.Services.DTOs.Workplace
{
    public class WorkplaceInfo
    {
        public int WorkplaceId { get; set; }
        public int Floor { get; set; }

        public string Zone { get; set; }

        public bool HasMonitor { get; set; }

        public bool HasDock { get; set; }

        public bool IsNearWindow { get; set; }

        public bool IsNearPrinter { get; set; }

        public bool IsAvailable { get; set; }
    }
}
