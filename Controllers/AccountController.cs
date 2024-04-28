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
    /// <summary>
    /// Controller for the user account (after successful authentication)
    /// </summary>
    public class AccountController : Controller
    {
        //User service object instance
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            //User service object initialization
            _userService = userService;
        }

        /// <summary>
        /// Responsible for displayng Login View.
        /// </summary>
        /// <returns>VIew => Login view</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Authenticates the user with the credentials passed in from the UI 
        /// </summary>
        /// <param name="user">email address and password fields set to the user model object from the UI</param>
        /// <returns>Authenticated User json object</returns>
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

        /// <summary>
        /// Logouts the logged-in user
        /// </summary>
        /// <returns>(Performs) The action of logging out and releases the claims</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// Occurs when a user tries to gain access witout authentication
        /// </summary>
        /// <returns>(Performs) The action of denying access</returns>
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}