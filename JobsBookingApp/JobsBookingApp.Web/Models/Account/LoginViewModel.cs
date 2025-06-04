using System.ComponentModel.DataAnnotations;

namespace JobsBookingApp.Web.Models.Account
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
