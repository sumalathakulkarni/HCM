
using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace HCM.Services
{
    public class DepartmentService : DBServiceBase, IDepartmentService
    {
        public DepartmentService(IConfiguration configuration) : base(configuration) { }
        public IList<DepartmentModel> GetAllDepartments()
        {
            var departments = new List<DepartmentModel>();
            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetAllDepartments);
            cmd.Connection = con;
            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DepartmentModel deapartment = new DepartmentModel()
                    {
                        DepartmentID = reader.GetInt32("DepartmentID"),
                        DepartmentName = reader.GetString("DepartmentName"),
                    };
                    departments.Add(deapartment);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return departments;
        }

        public DepartmentModel GetDepartmentById(int departmentID)
        {
            DepartmentModel department = null;

            var parameters = new Dictionary<string, object>
            {
                { "DepartmentID", departmentID }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetDepartmentByID, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    department = new DepartmentModel()
                    {
                        DepartmentID = reader.GetInt32("DepartmentID"),
                        DepartmentName = reader.GetString("DepartmentName"),
                        EmployeeCount = reader.GetInt32("EmployeeCount")
                    };
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }

            return department;
        }

        public int DeleteDepartment(int departmentID)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "DeptID", departmentID }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.DeleteDepartment, parameters);
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

        public int SaveDepartment(DepartmentModel dept)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "DepartmentName", dept.DepartmentName }
            };

            var con = GetDatabaseConnection();

            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.AddDepartment, parameters);
            if (dept.DepartmentID != 0)
            {
                parameters.Add("DeptID", dept.DepartmentID);
                cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.UpdateDepartmentName, parameters);
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
