﻿
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
