﻿namespace HCM.Utilities
{
    public class HCMConstants
    {
        public static string DBConnectionStringSetting = "HCMDatabaseConnection";
        
    }

    public class ProcedureNames
    {
        public static string ValidateCredentials = "ValidateCredentials";

        public static string GetAllEmployees = "GetAllEmployees";
        public static string GetEmployeeByID = "GetEmployeeByID";
        public static string GetEmployeeByEmailID = "GetEmployeeByEmailID";
        public static string DeleteEmployee = "DeleteEmployee";
        public static string AddEmployee = "AddAnEmployee";
        public static string UpdateEmployee = "UpdateEmployee";

        public static string GetEmployeeBenefits = "GetEmployeeBenefits";
        public static string UpdateEmployeeBenefits = "UpdateEmployeeBenefits";
        public static string GetManagersByDepartment = "GetManagersByDepartment";

        public static string GetAllDepartments = "GetAllDepartments";
        public static string DeleteDepartment = "DeleteDepartment";
        public static string GetDepartmentByID = "GetDepartmentByID";
        public static string AddDepartment = "AddNewDepartment";
        public static string UpdateDepartmentName = "UpdateDepartmentName";

        public static string GetAllRoles = "GetAllRoles";
        public static string GetRoleByID = "GetRoleByID";
        public static string DeleteRole = "DeleteRole";
        public static string AddRole = "AddNewRole";
        public static string UpdateRoleName = "UpdateRoleName";

        public static string GetAllSkills = "GetAllSkills";
        public static string GetSkillByID = "GetSkillByID";
        public static string DeleteSkill = "DeleteSkill";
        public static string AddSkill = "AddNewSkill";
        public static string UpdateSkillName = "UpdateSkillName";

        public static string GetEmployeeSkills = "GetEmployeeSkills";
        public static string AddEmployeeSkill = "AddEmployeeSkill";

        public static string GetEmployeePTODetails = "GetEmployeePTODetails";
        public static string GetAllPTOTypes = "GetAllPTOTypes";
        public static string ApplyPTO = "ApplyPTO";


    }
}
