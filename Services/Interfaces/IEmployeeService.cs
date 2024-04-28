using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    /// <summary>
    /// Service Contract for all the REST API calls for the Employee module views and functionalities.
    /// </summary>
    public interface IEmployeeService
    {
        IList<EmployeeModel> GetAllEmployees();
        EmployeeModel GetEmployeeByID(int employeeId);
        EmployeeModel GetEmployeeByEmail(string EmailAddress);
        public int DeleteEmployee(int employeeId);
        public int SaveEmployee(EmployeeModel employee);
        EmployeeBenefitsModel GetEmployeeBenefits(int employeeId);
        int SaveBenefits(EmployeeBenefitsModel benefits);
        EmpSkillsModel GetAllSkillsandEmployeeSkills(int employeeId);
        int SaveEmployeeSkills(EmpSkillsModel skills, int employeeId);
        PTOModel GetEmployeePTODetails(int employeeId);
        int ApplyPTO(PTOModel pto, int employeeId);
    }
}