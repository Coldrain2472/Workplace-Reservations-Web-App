namespace JobsBookingApp.Web.Models.Employee
{
    public class EmployeeListViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }

        public int TotalCount { get; set; }
    }

    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

       public string Username { get; set; }
    }
}
