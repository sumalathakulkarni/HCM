using HCM.Models;

namespace HCM.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Service Contract for all the REST API calls for the (Authenticated) User module views and functionalities.
        /// </summary>
        UserModel ValidateCredentials(UserModel user);
    }
}
