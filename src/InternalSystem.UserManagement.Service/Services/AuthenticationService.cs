using InternalSystem.UserManagement.Services.IServices;
using IntrenalSystem.UserManagement.Model.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Constructor
        public AuthenticationService()
        {

        }
        #endregion
        #region Methods 
        public async Task<JsonResponseModel<string>> ValidateUserCredentials(string userName, string password)
        {
            var response = new JsonResponseModel<string>();
            try
            {
                response.Result = "";
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = Helpers.ResponseMessage.ValidationSuccess;
                response.HasError = false;
                return response;
            }
            catch (Exception)
            {
                response.Result = null;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = Helpers.ResponseMessage.ValidationFailure;
                response.HasError = true;
                return response;
            }
        }

        #endregion
    }
}
