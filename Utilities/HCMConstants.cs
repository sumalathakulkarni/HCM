namespace HCM.Utilities
{
    public class HCMConstants
    {
        public static string DBConnectionStringSetting = "HCMDatabaseConnection";
        
    }

    public class ProcedureNames
    {
        //Stored-proc for validating login credentials and giving back the user object 
        public static string ValidateCredentials = "ValidateCredentials";

        #region EMPLOYEE related stored-procs
        public static string GetAllEmployees = "GetAllEmployees";
        public static string GetEmployeeByID = "GetEmployeeByID";
        public static string GetEmployeeByEmailID = "GetEmployeeByEmailID";
        public static string DeleteEmployee = "DeleteEmployee";
        public static string AddEmployee = "AddAnEmployee";
        public static string UpdateEmployee = "UpdateEmployee";
        #endregion

        #region Employee Benefits related stored-procs
        public static string GetEmployeeBenefits = "GetEmployeeBenefits";
        public static string UpdateEmployeeBenefits = "UpdateEmployeeBenefits";
        #endregion

        #region Department related stored-procs
        public static string GetAllDepartments = "GetAllDepartments";
        public static string DeleteDepartment = "DeleteDepartment";
        public static string AddDepartment = "AddNewDepartment";
        public static string UpdateDepartmentName = "UpdateDepartmentName";
        #endregion

        #region Role related stored-procs
        public static string GetAllRoles = "GetAllRoles";
        public static string DeleteRole = "DeleteRole";
        public static string AddRole = "AddNewRole";
        public static string UpdateRoleName = "UpdateRoleName";
        #endregion

        #region Skills related stored-procs
        public static string GetAllSkills = "GetAllSkills";
        public static string DeleteSkill = "DeleteSkill";
        public static string AddSkill = "AddNewSkill";
        public static string UpdateSkillName = "UpdateSkillName";
        #endregion

        #region EmployeeSkills related stored-procs
        public static string GetEmployeeSkills = "GetEmployeeSkills";
        public static string AddEmployeeSkill = "AddEmployeeSkill";
        #endregion

        #region Employee PTO related stored-procs
        public static string GetEmployeePTODetails = "GetEmployeePTODetails";
        public static string GetAllPTOTypes = "GetAllPTOTypes";
        public static string ApplyPTO = "ApplyPTO";
        #endregion
    }
}
