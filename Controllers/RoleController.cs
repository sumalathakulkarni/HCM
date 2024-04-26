using HCM.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HCM.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleService _RoleService;

        public RoleController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();
            _RoleService = serviceProvider.GetRequiredService<IRoleService>();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public PartialViewResult AddRole()
        {
            return new PartialViewResult
            {
                ViewName = "AddRole",
                ViewData = new ViewDataDictionary<RoleModel>(ViewData, new RoleModel())
            };
        }

        public PartialViewResult EditRole(RoleModel role)
        {
            return new PartialViewResult
            {
                ViewName = "EditRole",
                ViewData = new ViewDataDictionary<RoleModel>(ViewData, role)
            };
        }

        [HttpGet]
        public JsonResult GetAllRoles()
        {
            return Json(_RoleService.GetAllRoles());
        }

        [HttpPost]
        public JsonResult SaveRole(RoleModel role)
        {
            return Json(_RoleService.SaveRole(role));
        }

        [HttpPost]
        public JsonResult DeleteRole([FromQuery] int RoleId)
        {
            return Json(_RoleService.DeleteRole(RoleId));
        }
    }
}
