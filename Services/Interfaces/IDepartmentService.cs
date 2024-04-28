using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Service contract for all the REST API calls for the Department module views and functionalities. 
        /// </summary>
        /// <returns></returns>
        IList<DepartmentModel> GetAllDepartments();
        public int DeleteDepartment(int departmentID);
        public int SaveDepartment(DepartmentModel dept);

    }
}