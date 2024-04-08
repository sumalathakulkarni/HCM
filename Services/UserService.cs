using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using System.Data;

namespace HCM.Services
{
    public class UserService : DBServiceBase, IUserService
    {
        public UserService(IConfiguration configuration) : base(configuration)
        {
        }

        public UserModel ValidateCredentials(UserModel user)
        {
            var parameters = new Dictionary<string, object>
            {
                { "EmailAddress", user.Email },
                { "EmpPassword", user.Password }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.ValidateCredentials, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.EmployeeID = reader.IsDBNull("EmployeeID") ? 0 : reader.GetInt32("EmployeeID");
                    user.FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName");
                    user.LastName = reader.IsDBNull("LastName") ? string.Empty : reader.GetString("LastName");
                    user.Role = reader.IsDBNull("RoleName") ? string.Empty : reader.GetString("RoleName");
                    user.IsAuthenticated = true;
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return user;
        }
    }
}

