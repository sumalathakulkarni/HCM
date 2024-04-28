using HCM.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HCM.Controllers
{
    /// <summary>
    /// Controller for the views in the Department module.
    /// </summary>
    /// //The below tag makes sure that only the authenticated user has access.
    /// Any unauthenticated user is redirected to the login view
    [Authorize]  
    public class DepartmentController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        /// <summary>
        /// The department service instance.
        /// </summary>
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();
            //Initializig the department service instance.
            _departmentService = serviceProvider.GetRequiredService<IDepartmentService>();
        }

        /// <summary>
        /// Responsible for displaying the Departments list view.
        /// </summary>
        /// <returns>View => Departments list json</returns>
        public IActionResult Departments()
        {
            return View();
        }

        /// <summary>
        /// Responsible for displaying the Add New Department view.
        /// </summary>
        /// <returns>PartialViewResult json</returns>
        public PartialViewResult AddDepartment()
        {
            return new PartialViewResult
            {
                ViewName = "AddDepartment",
                ViewData = new ViewDataDictionary<DepartmentModel>(ViewData, new DepartmentModel())
            };
        }

        /// <summary>
        /// Responsible for displaying the Edit Department View.
        /// </summary>
        /// <param name="dept">Department model object</param>
        /// <returns>PartialViewResult json</returns>
        public PartialViewResult EditDepartment(DepartmentModel dept)
        {
            return new PartialViewResult
            {
                ViewName = "EditDepartment",
                ViewData = new ViewDataDictionary<DepartmentModel>(ViewData, dept)
            };
        }

        /// <summary>
        /// REST API call for fetching (GET) the list of all departments.
        /// </summary>
        /// <returns>List of departments json object</returns>
        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            return Json(_departmentService.GetAllDepartments());
        }

        /// <summary>
        /// REST API call for submitting (POST) the details of 'New' Department in case of Add New Department
        /// or existing department details in case of 'Edit' Department functionality.
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveDepartment(DepartmentModel dept)
        {
            return Json(_departmentService.SaveDepartment(dept));
        }

        /// <summary>
        /// REST API call (POST) for deleting a selected department -> submitting/ posting with the DepartmentID.
        /// </summary>
        /// <param name="departmentId">Selected department identifier</param>
        /// <returns>json success/ fail scenario</returns>
        [HttpPost]
        public JsonResult DeleteDepartment([FromQuery] int departmentId)
        {
            return Json(_departmentService.DeleteDepartment(departmentId));
        }
    }
}
