namespace HCM.Utilities
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
    }
}
