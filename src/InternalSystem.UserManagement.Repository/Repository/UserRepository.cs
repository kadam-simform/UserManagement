using InternalSystem.UserManagement.Database.DatabaseEntity;
using InternalSystem.UserManagement.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> SignInUser(string userName, string password)
        {
            
            return await _signInManager.PasswordSignInAsync(userName, password, false, false);
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            return await _userManager.CreateAsync(user,password);
        }
    }
}
