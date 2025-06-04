using System.Data.SqlTypes;

namespace JobsBookingApp.Repository.Interfaces.EmployeeFavorite
{
    public class EmployeeFavoriteFilter
    {
        public SqlInt32? EmployeeId { get; set; }

        public SqlInt32? WorkplaceId { get; set; }
    }
}
