using HCM.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HCM.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();
            _departmentService = serviceProvider.GetRequiredService<IDepartmentService>();
        }

        public IActionResult Departments()
        {
            return View();
        }

        public PartialViewResult AddDepartment()
        {
            return new PartialViewResult
            {
                ViewName = "AddDepartment",
                ViewData = new ViewDataDictionary<DepartmentModel>(ViewData, new DepartmentModel())
            };
        }

        public PartialViewResult EditDepartment(DepartmentModel dept)
        {
            return new PartialViewResult
            {
                ViewName = "EditDepartment",
                ViewData = new ViewDataDictionary<DepartmentModel>(ViewData, dept)
            };
        }

        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            return Json(_departmentService.GetAllDepartments());
        }

        [HttpPost]
        public JsonResult SaveDepartment(DepartmentModel dept)
        {
            return Json(_departmentService.SaveDepartment(dept));
        }

        [HttpPost]
        public JsonResult DeleteDepartment([FromQuery] int departmentId)
        {
            return Json(_departmentService.DeleteDepartment(departmentId));
        }
    }
}
