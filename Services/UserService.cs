using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using System.Data;

namespace HCM.Services
{
    /// <summary>
    /// Service for the Database Service calls for all the REST API calls of the (Authenticated) User/ Account module views and functionalities.
    /// </summary>
    public class UserService : DBServiceBase, IUserService
    {
        public UserService(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Responsible for checking if the user with the input credentials exists/ not exists in the database.
        /// </summary>
        /// <param name="user">UserModel object with the attributes set to the input field values from the UI.</param>
        /// <returns>UserModel object with the relevant details:
        /// a valid (authenticated) user or not, user role attribute values set with the values retrived from database</returns>
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

