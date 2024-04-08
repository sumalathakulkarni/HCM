using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    public interface IEmployeeService
    {
        IList<EmployeeModel> GetAllEmployees();
        EmployeeModel GetEmployeeByID(int employeeId);
        EmployeeModel GetEmployeeByEmail(string EmailAddress);
        public int DeleteEmployee(int employeeID);
        public int SaveEmployee(EmployeeModel employee);
        EmployeeBenefitsModel GetEmployeeBenefits(int employeeID);
        int SaveBenefits(EmployeeBenefitsModel benifits);
    }
}