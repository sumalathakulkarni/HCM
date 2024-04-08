
using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace HCM.Services
{
    public class RoleService : DBServiceBase, IRoleService
    {
        public RoleService(IConfiguration configuration) : base(configuration) { }
        public IList<RoleModel> GetAllRoles()
        {
            var Roles = new List<RoleModel>();
            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetAllRoles);
            cmd.Connection = con;
            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoleModel deapartment = new RoleModel()
                    {
                        RoleID = reader.GetInt32("RoleID"),
                        RoleName = reader.GetString("RoleName"),
                    };
                    Roles.Add(deapartment);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return Roles;
        }

        public RoleModel GetRoleById(int RoleID)
        {
            RoleModel Role = null;

            var parameters = new Dictionary<string, object>
            {
                { "RoleID", RoleID }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetRoleByID, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Role = new RoleModel()
                    {
                        RoleID = reader.GetInt32("RoleID"),
                        RoleName = reader.GetString("RoleName")
                    };
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }

            return Role;
        }

        public int DeleteRole(int RoleID)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "RoleID", RoleID }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.DeleteEmployee, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public int SaveRole(RoleModel dept)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "RoleName", dept.RoleName }
            };

            var con = GetDatabaseConnection();

            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.AddRole, parameters);
            if (dept.RoleID != 0)
            {
                parameters.Add("RID", dept.RoleID);
                cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.UpdateRoleName, parameters);
            }

            cmd.Connection = con;

            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}
