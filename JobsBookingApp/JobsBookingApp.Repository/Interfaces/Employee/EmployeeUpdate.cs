using System.Data.SqlTypes;

namespace JobsBookingApp.Repository.Interfaces.Employee
{
    public class EmployeeUpdate
    {
        public SqlString? Name { get; set; }

        public SqlString? Email { get; set; }

        public SqlString? Username { get; set; }

        public SqlString? Password { get; set; }
    }
}
