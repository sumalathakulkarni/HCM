using HCM.Models;

namespace HCM.Services.Interfaces
{
    public interface IUserService
    {
        UserModel ValidateCredentials(UserModel user);
    }
}
