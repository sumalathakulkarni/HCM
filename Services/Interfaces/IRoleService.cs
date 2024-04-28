using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    /// <summary>
    /// Service Contract for all the REST API calls for the Role module views and functionalities.
    /// </summary>
    public interface IRoleService
    {
        IList<RoleModel> GetAllRoles();
        public int DeleteRole(int roleID);
        public int SaveRole(RoleModel role);

    }
}