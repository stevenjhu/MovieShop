using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var userSuccess = await _accountService.ValidateUser(model);
            if (userSuccess != null && userSuccess.Id > 0)
            {
                // password matches
                // redirect to home page
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userSuccess.Email),
                    new Claim(ClaimTypes.NameIdentifier, userSuccess.Id.ToString()),
                    new Claim(ClaimTypes.Surname, userSuccess.LastName),
                    new Claim(ClaimTypes.GivenName, userSuccess.FirstName),
                    //new Claim("language", "english") key value pair
                };

                //identity object
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return LocalRedirect("~/");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            // showing empty register view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var userId = await _accountService.RegisterUser(model);

            if (userId > 0)
            {
                // redirect to login page
                return RedirectToAction("Login");
            }

            return View();
        }

        
    }
}
