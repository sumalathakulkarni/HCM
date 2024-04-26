
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
    public class EmployeeService : DBServiceBase, IEmployeeService
    {
        public EmployeeService(IConfiguration configuration) : base(configuration) { }
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

