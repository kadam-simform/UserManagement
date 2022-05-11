using AutoMapper;
using InternalSystem.UserManagement.Repository.IRepository;
using InternalSystem.UserManagement.Service.IServices;
using IntrenalSystem.UserManagement.Model.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            
        }
        public async Task<UserResponse> GetByEmail(string email)
        {
            var userFromDB = await _userRepository.GetByEmail(email);
            var userDTOToReturn = _mapper.Map<UserResponse>(userFromDB);
            return userDTOToReturn;
        }
    }
}
