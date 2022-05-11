using InternalSystem.UserManagement.Database.DatabaseEntity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<SignInResult> SignInUser(string userName, string password);
        public Task<IdentityResult> CreateUser(User user, string password);
    }
}
