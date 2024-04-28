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
    /// <summary>
    /// Controller for all the views under the Employee module.
    /// </summary>
    /// The below tag ensures that only the authenticated user has access to the views under the Employee module.
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService; //Employee Service instance declaration.
        private readonly IDepartmentService _departmentService; //Department Service instance declaration.
        private readonly IRoleService _roleService; //Role Service instance declaration.

        public EmployeeController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();

            // Employee Service instance initialization.
            _employeeService = serviceProvider.GetRequiredService<IEmployeeService>();

            // Deparment Service instance initialization.
            _departmentService = serviceProvider.GetRequiredService<IDepartmentService>();

            // Role Service instance initialization.
            _roleService = serviceProvider.GetRequiredService<IRoleService>();
        }

        #region Views
        /// <summary>
        /// Controller method responsible for displaying the Employees list View.
        /// </summary>
        /// <returns>View => List of Employees json</returns>
        public IActionResult EmployeeDirectory()
        {
            return View();
        }

        /// <summary>
        /// Responsible for displaying the Add New Employee View.
        /// </summary>
        /// <returns>View => Add New Employee json</returns>
        public IActionResult AddEmployee()
        {
            return View();
        }

        /// <summary>
        /// Responsible for displaying the Edit Employee View for the selected employee in the employees list.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the selected employee.</param>
        /// <returns>View => Edit Employee json</returns>
        public IActionResult EditEmployee([FromQuery] int employeeId)
        {
            if (employeeId == 0)
            {
                employeeId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));
            }
            var user = _employeeService.GetEmployeeByID(employeeId);
            return View(user);
        }

        /// <summary>
        /// Responsible for displaying the View Employee details for the selected employee in the employees list.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the selected employee.</param>
        /// <returns>PartialViewResult => View Employee details.</returns>
        public PartialViewResult ViewEmployee([FromQuery] int employeeId)
        {
            var empDetails = _employeeService.GetEmployeeByID(employeeId);

            return new PartialViewResult
            {
                ViewName = "ViewEmployee",
                ViewData = new ViewDataDictionary<EmployeeModel>(ViewData, empDetails)
            };
        }

        /// <summary>
        /// Responsible for displaying the View Benefits for the selected employee in the employees list.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the selected employee.</param>
        /// <returns>PartialViewResult => View Employee Benefits</returns>
        public PartialViewResult ViewBenefits([FromQuery] int employeeId)
        {
            EmployeeBenefitsModel empBenefits = _employeeService.GetEmployeeBenefits(employeeId);

            return new PartialViewResult
            {
                ViewName = "ViewBenefits",
                ViewData = new ViewDataDictionary<EmployeeBenefitsModel>(ViewData, empBenefits)
            };
        }

        /// <summary>
        /// Responsible for displaying the Edit Benefits view of the logged-in user.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the Logged-in User.</param>
        /// <returns>PartialViewResult => Edit Employee Benefits</returns>
        public PartialViewResult EditBenefits([FromQuery] int employeeId)
        {
            var empBenefit = _employeeService.GetEmployeeBenefits(employeeId);

            return new PartialViewResult
            {
                ViewName = "EditBenefits",
                ViewData = new ViewDataDictionary<EmployeeBenefitsModel>(ViewData, empBenefit)
            };
        }

        /// <summary>
        /// Responsible for displaying the PTO details of the logged-in user.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the logged-in user.</param>
        /// <returns>PartialViewResult => View Employee PTO</returns>
        public PartialViewResult ViewPTO([FromQuery] int employeeId)
        {
            PTOModel empPTO = _employeeService.GetEmployeePTODetails(employeeId);

            return new PartialViewResult
            {
                ViewName = "ViewPTO",
                ViewData = new ViewDataDictionary<PTOModel>(ViewData, empPTO)
            };
        }

        /// <summary>
        /// Responsible for displaying the Employee Skills view of the logged-in user.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the logged-in user.</param>
        /// <returns>PartialViewResult => View/ Update Employee Skills</returns>
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

        /// <summary>
        /// REST API call to fetch (GET) the list of all employees.
        /// </summary>
        /// <returns>List of employees json object.</returns>
        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            return Json(_employeeService.GetAllEmployees());
        }

        /// <summary>
        /// REST API call to check (GET) if an email address already exists in the existing employees' database. 
        /// This is a validation used before adding a new employee.
        /// This is important as emailaddress is one of the login credentials.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckEmployeeExists([FromQuery] string emailAddress)
        {
            var empDetails = _employeeService.GetEmployeeByEmail(emailAddress);
            var result = new { emailAddress = emailAddress, IsExisting = empDetails != null };
            return Json(result);

        }

        /// <summary>
        /// REST API call for saving (POST) the employee details: 
        /// either in case of Add New Employee/ Edit Employee (selected employee from the employees list) scenarios.
        /// </summary>
        /// <param name="employeeModel">EmployeeModel object with fields set with the input field values of 
        /// Add New Employee View/ Edit Employee View.
        /// </param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult SaveEmployee(EmployeeModel employeeModel)
        {
            return Json(_employeeService.SaveEmployee(employeeModel));
        }

        /// <summary>
        /// REST API call (POST) for deleting the selected employee from the employees list.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the selected employee.</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult DeleteEmployee([FromQuery] int employeeId)
        {
            return Json(_employeeService.DeleteEmployee(employeeId));
        }

        /// <summary>
        /// REST API call to submit (POST) the benefits' choices for the logged-in user/ employee.
        /// </summary>
        /// <param name="benefits">EmployeeBenefitsModel object</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult SaveBenefits(EmployeeBenefitsModel benefits)
        {
            return Json(_employeeService.SaveBenefits(benefits));
        }

        /// <summary>
        /// REST API call for submitting (POST) the skills list of the logged-in user/ employee.
        /// </summary>
        /// <param name="skills">EmpSkillsModel object.</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult SaveEmployeeSkills(EmpSkillsModel skills)
        {
            int employeeId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));
            return Json(_employeeService.SaveEmployeeSkills(skills, employeeId));
        }

        /// <summary>
        /// REST API call for submitting (POST) PTO request for the logged-in user/ employee.
        /// </summary>
        /// <param name="pto">PTOModel object</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult SavePTO(PTOModel pto)
        {
            int employeeId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.SerialNumber));
            return Json(_employeeService.ApplyPTO(pto, employeeId));
        }

        #endregion

        #region MasterData

        /// <summary>
        /// REST API call to fetch (GET) the list of all departments for loading in the department dropdown:
        /// Add New Employee/ Edit Employee functionalities.
        /// </summary>
        /// <returns>List of departments json object</returns>
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

        /// <summary>
        /// REST API call to fetch (GET) the list of all roles for loading the role dropdown:
        /// Add New Employee/ Edit Employee functionalities.
        /// </summary>
        /// <returns>List of roles json object</returns>
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

        /// <summary>
        /// Retrieves a list of all managers from the list of all employees for loading the manager dropdown:
        /// Add New Employee/ Edit Employee functionalities.
        /// </summary>
        /// <returns>List of managers json object</returns>
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
