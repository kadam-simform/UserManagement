using IntrenalSystem.UserManagement.Model.APIResponseModels;

namespace InternalSystem.UserManagement.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<JsonResponseModel<string>> ValidateUserCredentials(string userName, string password);
    }
}