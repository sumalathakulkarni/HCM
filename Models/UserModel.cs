using System.Data;

namespace HCM.Models
{
    public class UserModel
    {
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
