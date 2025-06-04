using JobsBookingApp.Services.Interfaces.Authentication;
using JobsBookingApp.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace JobsBookingApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(IAuthenticationService _authenticationService)
        {
            authenticationService = _authenticationService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel 
            { 
                ReturnUrl = returnUrl 
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await authenticationService.LoginAsync(new Services.DTOs.Authentication.LoginRequest
            {
                Username = model.Username,
                Password = model.Password
            });

            if (result.Success)
            {
                HttpContext.Session.SetInt32("UserId", result.EmployeeId.Value);
                HttpContext.Session.SetString("UserName", result.Name);

                if (!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", nameof(HomeController));
            }


            ViewData["ErrorMessage"] = result.ErrorMessage ?? "Invalid username or password";
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

