using IntrenalSystem.UserManagement.Model.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Service.IServices
{
    public interface IUserService
    {
        Task<UserResponse> GetByEmail(string email);
    }
}
