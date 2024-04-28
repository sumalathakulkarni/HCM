using HCM.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HCM.Controllers
{
    /// <summary>
    /// Controller for all the views under the Role module.
    /// </summary>
    /// The below tag ensures that only the authenticated user has access to the views under the Role module.
    [Authorize]
    public class RoleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleService _RoleService; //Role Service instance declaration.

        public RoleController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();

            //Role Serive instance initialization.
            _RoleService = serviceProvider.GetRequiredService<IRoleService>();
        }

        /// <summary>
        /// Responsible for displaying the Roles list View.
        /// </summary>
        /// <returns>View => Roles list json</returns>
        public IActionResult Roles()
        {
            return View();
        }

        /// <summary>
        /// Responsible for displaying the Add New Role partial view (pop-up box).
        /// </summary>
        /// <returns>PartialViewResult => Add New Role</returns>
        public PartialViewResult AddRole()
        {
            return new PartialViewResult
            {
                ViewName = "AddRole",
                ViewData = new ViewDataDictionary<RoleModel>(ViewData, new RoleModel())
            };
        }

        /// <summary>
        /// Responsible for displaying the Edit Role partial view (pop-up box).
        /// </summary>
        /// <param name="role">RoleModel object with RoleID field value => selected role's roleid from the roles list</param>
        /// <returns>PartialViewResult => Edit Role</returns>
        public PartialViewResult EditRole(RoleModel role)
        {
            return new PartialViewResult
            {
                ViewName = "EditRole",
                ViewData = new ViewDataDictionary<RoleModel>(ViewData, role)
            };
        }

        /// <summary>
        /// REST API call to fetch (GET) the list of all roles.
        /// </summary>
        /// <returns>List of role json</returns>
        [HttpGet]
        public JsonResult GetAllRoles()
        {
            return Json(_RoleService.GetAllRoles());
        }

        /// <summary>
        /// REST API call to submit the details of the role for Add New Role/ Update Role functionalities.
        /// </summary>
        /// <param name="role">RoleModel object with field values set from the iput field values.</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult SaveRole(RoleModel role)
        {
            return Json(_RoleService.SaveRole(role));
        }

        /// <summary>
        /// REST API call for deleting a selected role (POST) from the list of roles.
        /// </summary>
        /// <param name="RoleId">RoleID of the selected role.</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult DeleteRole([FromQuery] int RoleId)
        {
            return Json(_RoleService.DeleteRole(RoleId));
        }
    }
}
