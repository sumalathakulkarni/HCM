using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    public interface IDepartmentService
    {
        IList<DepartmentModel> GetAllDepartments();
        public int DeleteDepartment(int departmentID);
        public int SaveDepartment(DepartmentModel dept);

    }
}