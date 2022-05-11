using AutoMapper;
using InternalSystem.UserManagement.API.Helpers;
using InternalSystem.UserManagement.Service.IServices;
using InternalSystem.UserManagement.Services.IServices;
using IntrenalSystem.UserManagement.Model.APIRequestModels;
using IntrenalSystem.UserManagement.Model.APIResponseModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InternalSystem.UserManagement.API.Controllers.V1
{
    [Route("v{version:apiVersion}/api/authentication")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        #region Constructor
        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, IMapper mapper)
        {
            this._authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region ActionMethods
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authenticationRequestBody)
        {
            var response = new JsonResponseModel<AuthenticationResponse>();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Message = ModelState.GetErrors();
                    response.HasError = true;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return this.GetResponse(response);
                }
                var userFromStore = await _userService.GetByEmail(authenticationRequestBody.UserName!);
                if (userFromStore == null)
                {
                    response.HasError = true;
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.Result = null;
                    response.Message = Constants.AUTHENTICATION_FAILURE;
                    return this.GetResponse(response);
                }

                //Code to validate username/password 
                var validate = await _authenticationService.ValidateUserCredentials(
                   authenticationRequestBody!.UserName!,
                   authenticationRequestBody!.Password!);

                if (!validate.Succeeded)
                {
                    response.HasError = true;
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.Result = null;
                    response.Message = Constants.AUTHENTICATION_FAILURE;
                    return this.GetResponse(response);
                }
                else
                {

                    var responseToReturn = new AuthenticationResponse()
                    {
                        Token = await _authenticationService.GenerateToken(_mapper.Map<UserRequest>(userFromStore))
                    };
                    if (responseToReturn.Token == null)
                    {
                        response.HasError = true;
                        response.StatusCode = HttpStatusCode.InternalServerError;
                        response.Result = null;
                        response.Message = Constants.ERROR;
                        return this.GetResponse(response);
                    }
                    else
                    {
                        HttpContext.Response.Headers.Add("access_token", responseToReturn.Token);
                        response.HasError = false;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Result = null;
                        response.Message = Constants.AUTHENTICATION_SUCCESS;
                        return this.GetResponse(response);
                    }

                }
            }
            catch (Exception)
            {
                response.HasError = true;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Result = null;
                response.Message = Constants.ERROR;
                return this.GetResponse(response);
            }
            
        }
        #endregion
    }
}
