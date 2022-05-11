using IntrenalSystem.UserManagement.Model.APIRequestModels;
using IntrenalSystem.UserManagement.Model.APIResponseModels;
using Microsoft.AspNetCore.Identity;

namespace InternalSystem.UserManagement.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<SignInResult> ValidateUserCredentials(string userName, string password);
        Task<string> GenerateToken(UserRequest user);
    }
}