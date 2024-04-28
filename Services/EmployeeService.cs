
using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Runtime.Serialization;

namespace HCM.Services
{
    /// <summary>
    /// Service for the Database Service calls for all the REST API calls of the Employee module views and functionalities.
    /// </summary>
    public class EmployeeService : DBServiceBase, IEmployeeService
    {
        public EmployeeService(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Responsible for retrieving the list of all the active employee records from the database.
        /// </summary>
        /// <returns>List of EmployeeModel objects</returns>
        public IList<EmployeeModel> GetAllEmployees()
        {
            var employees = new List<EmployeeModel>();

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetAllEmployees);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeModel employee = new EmployeeModel()
                    {
                        EmployeeID = reader.IsDBNull("EmployeeID") ? 0 : reader.GetInt32("EmployeeID"),
                        FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName"),
                        MiddleName = reader.IsDBNull("MiddleName") ? string.Empty : reader.GetString("MiddleName"),
                        LastName = reader.IsDBNull("LastName") ? string.Empty : reader.GetString("LastName"),
                        EmailAddress = reader.IsDBNull("EmailAddress") ? string.Empty : reader.GetString("EmailAddress"),
                        DepartmentName = reader.IsDBNull("DepartmentName") ? string.Empty : reader.GetString("DepartmentName"),
                        RoleName = reader.IsDBNull("RoleName") ? string.Empty : reader.GetString("RoleName"),
                        Manager = reader.IsDBNull("Manager") ? string.Empty : reader.GetString("Manager"),
                        Salary = reader.IsDBNull("Salary") ? 0 : reader.GetDouble("Salary"),
                        BiWeeklyPayAmount = reader.IsDBNull("BiWeeklyPayAmount") ? 0 : reader.GetDouble("BiWeeklyPayAmount"),
                        BankName = reader.IsDBNull("BankName") ? string.Empty : reader.GetString("BankName"),
                        AccountNumber = reader.IsDBNull("AccountNumber") ? string.Empty : reader.GetString("AccountNumber"),
                        RoutingNumber = reader.IsDBNull("RoutingNumber") ? string.Empty : reader.GetString("RoutingNumber")
                    };
                    employees.Add(employee);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return employees;
        }

        /// <summary>
        /// Responsible for retrieving a specific employee record from the database.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the specific employee</param>
        /// <returns>EmployeeModel object of the specific employee.</returns>
        public EmployeeModel GetEmployeeByID(int employeeId)
        {
            EmployeeModel employee = null;

            var parameters = new Dictionary<string, object>
            {
                { "EmployeeID", employeeId }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetEmployeeByID, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee = new EmployeeModel()
                    {
                        EmployeeID = reader.IsDBNull("EmployeeID") ? 0 : reader.GetInt32("EmployeeID"),
                        FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName"),
                        MiddleName = reader.IsDBNull("MiddleName") ? string.Empty : reader.GetString("MiddleName"),
                        LastName = reader.IsDBNull("LastName") ? string.Empty : reader.GetString("LastName"),
                        EmailAddress = reader.IsDBNull("EmailAddress") ? string.Empty : reader.GetString("EmailAddress"),
                        JoinDate = reader.IsDBNull("JoinDate") ? DateTime.MinValue : reader.GetDateTime("JoinDate").Date,
                        DepartmentID = reader.IsDBNull("DepartmentID") ? 0 : reader.GetInt32("DepartmentID"),
                        DepartmentName = reader.IsDBNull("DepartmentName") ? string.Empty : reader.GetString("DepartmentName"),
                        RoleID = reader.IsDBNull("RoleID") ? 0 : reader.GetInt32("RoleID"),
                        RoleName = reader.IsDBNull("RoleName") ? string.Empty : reader.GetString("RoleName"),
                        ManagerID = reader.IsDBNull("ManagerID") ? 0 : reader.GetInt32("ManagerID"),
                        Manager = reader.IsDBNull("Manager") ? string.Empty : reader.GetString("Manager"),
                        Salary = reader.IsDBNull("Salary") ? 0 : reader.GetDouble("Salary"),
                        BiWeeklyPayAmount = reader.IsDBNull("BiWeeklyPayAmount") ? 0 : reader.GetDouble("BiWeeklyPayAmount"),
                        BankName = reader.IsDBNull("BankName") ? string.Empty : reader.GetString("BankName"),
                        AccountNumber = reader.IsDBNull("AccountNumber") ? string.Empty : reader.GetString("AccountNumber"),
                        RoutingNumber = reader.IsDBNull("RoutingNumber") ? string.Empty : reader.GetString("RoutingNumber"),
                    };
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return employee;
        }

        /// <summary>
        /// Responsible for retrieving the employee record of a specific employee 
        /// having the input emailaddress from the database.
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <returns>EmployeeModel object of the queried employee</returns>
        public EmployeeModel GetEmployeeByEmail(string EmailAddress)
        {
            EmployeeModel employee = null;

            var parameters = new Dictionary<string, object>
            {
                { "EmailAddress", EmailAddress }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetEmployeeByEmailID, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee = new EmployeeModel()
                    {
                        EmployeeID = reader.IsDBNull("EmployeeID") ? 0 : reader.GetInt32("EmployeeID"),
                        FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName"),
                        MiddleName = reader.IsDBNull("MiddleName") ? string.Empty : reader.GetString("MiddleName"),
                        LastName = reader.IsDBNull("LastName") ? string.Empty : reader.GetString("LastName"),
                        EmailAddress = reader.IsDBNull("EmailAddress") ? string.Empty : reader.GetString("EmailAddress"),
                        JoinDate = reader.IsDBNull("JoinDate") ? DateTime.MinValue : reader.GetDateTime("JoinDate").Date,
                        DepartmentID = reader.IsDBNull("DepartmentID") ? 0 : reader.GetInt32("DepartmentID"),
                        DepartmentName = reader.IsDBNull("DepartmentName") ? string.Empty : reader.GetString("DepartmentName"),
                        RoleID = reader.IsDBNull("RoleID") ? 0 : reader.GetInt32("RoleID"),
                        RoleName = reader.IsDBNull("RoleName") ? string.Empty : reader.GetString("RoleName"),
                        ManagerID = reader.IsDBNull("ManagerID") ? 0 : reader.GetInt32("ManagerID"),
                        Manager = reader.IsDBNull("Manager") ? string.Empty : reader.GetString("Manager"),
                        Salary = reader.IsDBNull("Salary") ? 0 : reader.GetDouble("Salary"),
                        BiWeeklyPayAmount = reader.IsDBNull("BiWeeklyPayAmount") ? 0 : reader.GetDouble("BiWeeklyPayAmount"),
                        BankName = reader.IsDBNull("BankName") ? string.Empty : reader.GetString("BankName"),
                        AccountNumber = reader.IsDBNull("AccountNumber") ? string.Empty : reader.GetString("AccountNumber"),
                        RoutingNumber = reader.IsDBNull("RoutingNumber") ? string.Empty : reader.GetString("RoutingNumber"),
                    };
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return employee;
        }

        /// <summary>
        /// Responsible for deleting a specific employee from the database.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>result success/ failure</returns>
        public int DeleteEmployee(int employeeId)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "EmployeeID", employeeId }
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

        /// <summary>
        /// Responsible foe saving the employee details in the "Add New Employee" / "Edit Employee" scenarios.
        /// </summary>
        /// <param name="employee">EmployeeModel object 
        /// with the attribute values set from the input field values from the UI.</param>
        /// <returns>result success/ failure</returns>
        public int SaveEmployee(EmployeeModel employee)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "FirstName", employee.FirstName },
                { "MiddleName", employee.MiddleName },
                { "LastName", employee.LastName},
                { "EmailAddress", employee.EmailAddress },
                { "EmpPassword", employee.Password},
                { "DepartmentID", employee.DepartmentID },
                { "RoleID", employee.RoleID},
                { "ManagerID", employee.ManagerID},
                { "Salary", employee.Salary},
                { "BiWeeklyPayAmount", employee.BiWeeklyPayAmount},
                { "BankName", employee.BankName},
                { "AccountNumber", employee.AccountNumber},
                { "RoutingNumber", employee.RoutingNumber},
            };

            var con = GetDatabaseConnection();

            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.AddEmployee, parameters);
            if (employee.EmployeeID != 0)
            {
                parameters.Add("EmpID", employee.EmployeeID);
                cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.UpdateEmployee, parameters);
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

        /// <summary>
        /// Responsible for retrieving the specific Employee's benefits' choices already saved from the database.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the specific employee.</param>
        /// <returns>EmployeeBenefitsModel object 
        /// with the attribute values set to the benefits' choice values retreived from the database.</returns>
        public EmployeeBenefitsModel GetEmployeeBenefits(int employeeId)
        {
            EmployeeBenefitsModel empBenefits = null;

            var parameters = new Dictionary<string, object>
            {
                { "EmpID", employeeId }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetEmployeeBenefits, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    empBenefits = new EmployeeBenefitsModel()
                    {
                        EmployeeMedical = reader.IsDBNull("EmployeeMedical") ? false : reader.GetBoolean("EmployeeMedical"),
                        EmployeeVision = reader.IsDBNull("EmployeeVision") ? false : reader.GetBoolean("EmployeeVision"),
                        EmployeeDental = reader.IsDBNull("EmployeeDental") ? false : reader.GetBoolean("EmployeeDental"),
                        EmployeeLife = reader.IsDBNull("EmployeeLife") ? false : reader.GetBoolean("EmployeeLife"),
                        Employee401K = reader.IsDBNull("Employee401K") ? false : reader.GetBoolean("Employee401K"),
                    };
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return empBenefits;
        }

        /// <summary>
        /// Responsible for saving/ updating the logged-in user's benefits' choices to the database.
        /// </summary>
        /// <param name="benefits">EmployeeBenefitsModel object 
        /// with the attributes values set from the input field values from the UI.</param>
        /// <returns>result success/ failure</returns>
        public int SaveBenefits(EmployeeBenefitsModel benefits)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "EmpID", benefits.EmployeeID },
                { "EmpMedical", benefits.EmployeeMedical },
                { "EmpVision", benefits.EmployeeVision },
                { "EmpDental", benefits.EmployeeDental },
                { "EmpLife", benefits.EmployeeLife },
                { "Emp401K", benefits.Employee401K }
            };

            var con = GetDatabaseConnection();

            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.UpdateEmployeeBenefits, parameters);
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
        /// Responsible for retrieving the list of all skills to choose from and existing employee skills from the database
        /// to display in the "My Skills" view.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the logged-in user.</param>
        /// <returns></returns>
        public EmpSkillsModel GetAllSkillsandEmployeeSkills(int employeeId)
        {
            EmpSkillsModel skills = new EmpSkillsModel();
            skills.AllSkills.Add(new SelectListItem { Text = "-- Available Skills --", Value = "-1", Selected = true });
            skills.EmployeeSkills.Add(new SelectListItem { Text = "-- Selected Skills --", Value = "-1", Selected = true });

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetAllSkills);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var skill = new SelectListItem()
                    {
                        Value = reader.GetInt32("SkillId").ToString(),
                        Text = reader.GetString("SkillName")
                    };
                    skills.AllSkills.Add(skill);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }

            var parameters = new Dictionary<string, object>
            {
                { "EmpID", employeeId }
            };

            cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetEmployeeSkills, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var skill = new SelectListItem()
                    {
                        Value = reader.GetInt32("SkillId").ToString(),
                        Text = reader.GetString("SkillName")
                    };
                    skills.EmployeeSkills.Add(skill);
                    skills.AllSkills.Remove(skills.AllSkills.First<SelectListItem>(sk => sk.Value == skill.Value));
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return skills;
        }

        /// <summary>
        /// Responsible for saving/ updating the logged-in user's skills to the database.
        /// </summary>
        /// <param name="skills">List of EmpSkillModel object</param>
        /// <param name="employeeId">EmployeeID of the logged-in user.</param>
        /// <returns>result success/ failure</returns>
        public int SaveEmployeeSkills(EmpSkillsModel skills, int employeeId)
        {
            var result = 0;

            foreach (var skill in skills.EmployeeSkills)
            {
                var parameters = new Dictionary<string, object>
                {
                    { "EmpSkillID", skill.Value },
                    { "EmpID", employeeId }
                };

                var con = GetDatabaseConnection();

                var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.AddEmployeeSkill, parameters);
                cmd.Connection = con;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = 1;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Responsible for retrieving the list of all PTOType for displaying in the "View/ Apply PTO" view
        /// </summary>
        /// <returns>Dropdown list items of PTOType</returns>
        private List<SelectListItem> GetAllPTOTypes()
        {
            var allPTOTypes = new List<SelectListItem>();
            allPTOTypes.Add(new SelectListItem { Text = "-- PTO Type --", Value = "-1", Selected = true });

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetAllPTOTypes);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var skill = new SelectListItem()
                    {
                        Value = reader.GetInt32("PTOTypeID").ToString(),
                        Text = reader.GetString("PTOTypeName")
                    };
                    allPTOTypes.Add(skill);
                }
                reader.Close();

            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }

            return allPTOTypes;
        }

        /// <summary>
        /// Responsible for retrieving the relevant detasil like Manager, LeaveBalance
        /// for displaying in the "View/ Apply PTO" view.
        /// </summary>
        /// <param name="employeeId">EmployeeID of the logged-in user.</param>
        /// <returns>PTOModel object</returns>
        public PTOModel GetEmployeePTODetails(int employeeId)
        {
            PTOModel empPTO = new PTOModel();

            var parameters = new Dictionary<string, object>
            {
                { "EmpID", employeeId }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetEmployeePTODetails, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    empPTO = new PTOModel()
                    {
                        LeaveBalance = reader.IsDBNull("LeaveBalance") ? 0 : reader.GetInt32("LeaveBalance"),
                        ManagerID = reader.IsDBNull("ManagerID") ? 0 : reader.GetInt32("ManagerID"),
                        ManagerName = reader.IsDBNull("ManagerName") ? string.Empty : reader.GetString("ManagerName"),
                    };
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }

            empPTO.AllPTOTypes = GetAllPTOTypes();

            return empPTO;
        }

        /// <summary>
        /// Responsible for updating the EmployeePaidTimeOff in the database. 
        /// </summary>
        /// <param name="pto">PTOModel object with the attributes set from the input field values.</param>
        /// <param name="employeeId">EmployeeID of the logged-in user.</param>
        /// <returns>result success/ failure</returns>
        public int ApplyPTO(PTOModel pto, int employeeId)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "EmpID", employeeId },
                { "PTOTypeID", pto.PTOTypeID },
                { "StartDate", pto.StartDate.ToString("yyyy-MM-dd") },
                { "EndDate", pto.EndDate.ToString("yyyy-MM-dd") },
                { "NumDays", pto.NumDays },
                { "Reason", pto.Reason },
                { "ManagerID", pto.ManagerID}
            };

            var con = GetDatabaseConnection();

            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.ApplyPTO, parameters);
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

