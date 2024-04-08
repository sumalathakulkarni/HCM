namespace HCM.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime JoinDate { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public string Manager { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public string Password { get; set; }
        public Double Salary { get; set; }
        public Double BiWeeklyPayAmount { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }

    }
}
