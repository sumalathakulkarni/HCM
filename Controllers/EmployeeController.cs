using HCM.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;
using System.Security.Claims;

namespace HCM.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IRoleService _roleService;

        public EmployeeController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();
            _employeeService = serviceProvider.GetRequiredService<IEmployeeService>();
            _departmentService = serviceProvider.GetRequiredService<IDepartmentService>();
            _roleService = serviceProvider.GetRequiredService<IRoleService>();
        }

        #region Views
        public IActionResult EmployeeDirectory()
        {
            return View();
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
        public IActionResult EditEmployee([FromQuery] int employeeId)
        {
            if (employeeId == 0)
            {
                employeeId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));
            }
            var user = _employeeService.GetEmployeeByID(employeeId);
            return View(user);
        }

        public PartialViewResult ViewEmployee([FromQuery] int employeeId)
        {
            var empDetails = _employeeService.GetEmployeeByID(employeeId);

            return new PartialViewResult
            {
                ViewName = "ViewEmployee",
                ViewData = new ViewDataDictionary<EmployeeModel>(ViewData, empDetails)
            };
        }
        public PartialViewResult ViewBenefits([FromQuery] int employeeId)
        {
            EmployeeBenefitsModel empBenefits = _employeeService.GetEmployeeBenefits(employeeId);

            return new PartialViewResult
            {
                ViewName = "ViewBenefits",
                ViewData = new ViewDataDictionary<EmployeeBenefitsModel>(ViewData, empBenefits)
            };
        }

        public PartialViewResult EditBenefits([FromQuery] int employeeId)
        {
            var empBenefit = _employeeService.GetEmployeeBenefits(employeeId);

            return new PartialViewResult
            {
                ViewName = "EditBenefits",
                ViewData = new ViewDataDictionary<EmployeeBenefitsModel>(ViewData, empBenefit)
            };
        }
        public PartialViewResult ViewPTO([FromQuery] int employeeId)
        {
            PTOModel empPTO = _employeeService.GetEmployeePTODetails(employeeId);

            return new PartialViewResult
            {
                ViewName = "ViewPTO",
                ViewData = new ViewDataDictionary<PTOModel>(ViewData, empPTO)
            };
        }

        public PartialViewResult EmployeeSkills([FromQuery] int employeeId)
        {
            var skillsObj = _employeeService.GetAllSkillsandEmployeeSkills(employeeId);

            EmpSkillsModel skills = new EmpSkillsModel() { AllSkills = skillsObj.AllSkills, EmployeeSkills = skillsObj.EmployeeSkills };

            return new PartialViewResult
            {
                ViewName = "EmployeeSkills",
                ViewData = new ViewDataDictionary<EmpSkillsModel>(ViewData, skills)
            };
        }

        #endregion

        #region API Calls

        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            return Json(_employeeService.GetAllEmployees());
        }

        [HttpGet]
        public JsonResult CheckEmployeeExists([FromQuery] string emailAddress)
        {
            var empDetails = _employeeService.GetEmployeeByEmail(emailAddress);
            var result = new { emailAddress = emailAddress, IsExisting = empDetails != null };
            return Json(result);

        }

        [HttpPost]
        public JsonResult SaveEmployee(EmployeeModel employeeModel)
        {
            return Json(_employeeService.SaveEmployee(employeeModel));
        }

        [HttpPost]
        public JsonResult DeleteEmployee([FromQuery] int employeeId)
        {
            return Json(_employeeService.DeleteEmployee(employeeId));
        }

        [HttpPost]
        public JsonResult SaveBenefits(EmployeeBenefitsModel benefits)
        {
            return Json(_employeeService.SaveBenefits(benefits));
        }

        [HttpPost]
        public JsonResult SaveEmployeeSkills(EmpSkillsModel skills)
        {
            int employeeId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));
            return Json(_employeeService.SaveEmployeeSkills(skills, employeeId));
        }
        [HttpPost]
        public JsonResult SavePTO(PTOModel pto)
        {
            int employeeId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));
            return Json(_employeeService.ApplyPTO(pto, employeeId));
        }

        #endregion

        #region MasterData

        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            var data = _departmentService.GetAllDepartments();
            var Departments = (from dept in data
                               select new SelectListItem
                               {
                                   Text = dept.DepartmentName,
                                   Value = dept.DepartmentID.ToString(),
                                   Selected = false
                               });
            return Json(Departments);
        }

        [HttpGet]
        public JsonResult GetAllRoles()
        {
            var data = _roleService.GetAllRoles();
            var Departments = (from role in data
                               select new SelectListItem
                               {
                                   Text = role.RoleName,
                                   Value = role.RoleID.ToString(),
                                   Selected = false
                               });
            return Json(Departments);
        }
        public JsonResult GetallManagers()
        {
            var data = _employeeService.GetAllEmployees().Where<EmployeeModel>(emp => emp.RoleName.ToLower().Contains("manager"));
            var managers = (from emp in data
                            select new SelectListItem
                            {
                                Text = $"{emp.FirstName} {emp.LastName}",
                                Value = emp.EmployeeID.ToString(),
                                Selected = false
                            });
            return Json(managers);
        }
        #endregion
    }
}
