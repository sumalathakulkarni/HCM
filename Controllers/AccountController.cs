using HCM.Models;
using HCM.Services;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MySqlX.XDevAPI;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Claims;
using ZstdSharp.Unsafe;

namespace HCM.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public JsonResult Authenticate(UserModel user)
        {
            //Check credentials and assign roles
            JsonResult result = Json(new { emailAddress = user.Email, IAuthenticated = false });

            user = _userService.ValidateCredentials(user);
            if (user.IsAuthenticated)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.SerialNumber, user.EmployeeID.ToString()),
                    new Claim(ClaimTypes.Role,user.Role),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());
                user.Password = string.Empty;

                result = Json(new { firstName = user.FirstName, lastName = user.LastName, emailAddress = user.Email, IsAuthenticated = true });
            }

            return Json(result);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}