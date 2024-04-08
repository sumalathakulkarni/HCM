using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    public interface IRoleService
    {
        IList<RoleModel> GetAllRoles();
        RoleModel GetRoleById(int roleID);
        public int DeleteRole(int roleID);
        public int SaveRole(RoleModel role);

    }
}