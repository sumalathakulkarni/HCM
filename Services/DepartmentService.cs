
using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace HCM.Services
{
    /// <summary>
    /// Service for the Database Service calls for all the REST API calls of the Department module views and functionalities.
    /// </summary>
    public class DepartmentService : DBServiceBase, IDepartmentService
    {
        public DepartmentService(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Responsible for retreiving the list of all departments from the database.
        /// </summary>
        /// <returns>List of DepartmentModel objects.</returns>
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
                    DepartmentModel department = new DepartmentModel()
                    {
                        DepartmentID = reader.GetInt32("DepartmentID"),
                        DepartmentName = reader.GetString("DepartmentName"),
                    };
                    departments.Add(department);
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

        /// <summary>
        /// Responsible for deleting the selected department from the database.
        /// </summary>
        /// <param name="departmentID">DepartmentID of the selected department from the list of departments.</param>
        /// <returns>result success/ failure</returns>
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

        /// <summary>
        /// Responsible for saving the department details to the database in Add New Department/ Edit Department scenarios.
        /// </summary>
        /// <param name="dept">DepartmentModel object with the attributes set from the input field values.</param>
        /// <returns>result success/ failure</returns>
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
