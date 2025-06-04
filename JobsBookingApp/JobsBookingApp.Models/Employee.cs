using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 120 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 40 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(256)]
        public string Password { get; set; }
    }
}
