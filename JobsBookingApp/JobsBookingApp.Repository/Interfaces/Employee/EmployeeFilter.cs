using System.Data.SqlTypes;

namespace JobsBookingApp.Repository.Interfaces.Employee
{
    public class EmployeeFilter
    {
        public SqlString? Email { get; set; }

        public SqlString? Username { get; set; }
    }
}
