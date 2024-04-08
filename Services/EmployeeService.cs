
using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;

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
        public int DeleteEmployee(int employeeID)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "EmployeeID", employeeID }
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
    }
}
