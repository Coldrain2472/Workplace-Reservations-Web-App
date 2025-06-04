using System.Data.SqlTypes;

namespace JobsBookingApp.Repository.Interfaces.Workplace
{
    public class WorkplaceFilter
    {
        public SqlInt32? Floor { get; set; }

        public SqlString? Zone { get; set; }

        public SqlBoolean? HasMonitor { get; set; }

        public SqlBoolean? HasDock { get; set; }

        public SqlBoolean? IsNearWindow { get; set; }

        public SqlBoolean? IsNearPrinter { get; set; }

        public SqlBoolean? IsAvailable { get; set; }
    }
}
