using AutoMapper;
using InternalSystem.UserManagement.Repository.IRepository;
using InternalSystem.UserManagement.Services.IServices;
using IntrenalSystem.UserManagement.Model.APIRequestModels;
using IntrenalSystem.UserManagement.Model.APIResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        #region Constructor
        public AuthenticationService(IConfiguration config, IUserRepository userRepository, IMapper mapper)
        {
            this._config = config;
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        #endregion

        #region Methods 
        public async Task<string> GenerateToken(UserRequest user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              claims: new Claim[] {
                  new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
              },
              expires: DateTime.UtcNow.AddDays(1),
              signingCredentials: credentials);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        public async Task<SignInResult> ValidateUserCredentials(string userName, string password)
        {
            return await _userRepository.SignInUser(userName, password);
        }

        #endregion
    }
}
