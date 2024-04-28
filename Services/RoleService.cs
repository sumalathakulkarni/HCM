
using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace HCM.Services
{
    /// <summary>
    /// Service for the Database Service calls for all the REST API calls of the Role module views and functionalities.
    /// </summary>
    public class RoleService : DBServiceBase, IRoleService
    {
        public RoleService(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Responsible for retrieving the list of all roles from the database.
        /// </summary>
        /// <returns>List of RoleModel objects.</returns>
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
                    RoleModel role = new RoleModel()
                    {
                        RoleID = reader.GetInt32("RoleID"),
                        RoleName = reader.GetString("RoleName"),
                    };
                    Roles.Add(role);
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

        /// <summary>
        /// Responsible for deleting a specific Role record from the database.
        /// </summary>
        /// <param name="RoleID">RoleID of the specific role.</param>
        /// <returns>result success/ failure</returns>
        public int DeleteRole(int RoleID)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "RoleID", RoleID }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.DeleteRole, parameters);
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

        /// <summary>
        /// Responsible for saving the Role details to the database in the Add New Role/ Edit Role scenarios.
        /// </summary>
        /// <param name="role">RoleModel object with the attribute values set from the input field values from the UI.</param>
        /// <returns>result success/ failure</returns>
        public int SaveRole(RoleModel role)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "RoleName", role.RoleName }
            };

            var con = GetDatabaseConnection();

            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.AddRole, parameters);
            if (role.RoleID != 0)
            {
                parameters.Add("RID", role.RoleID);
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
