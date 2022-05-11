using AutoMapper;
using InternalSystem.UserManagement.Database.DatabaseEntity;
using IntrenalSystem.UserManagement.Model.APIRequestModels;
using IntrenalSystem.UserManagement.Model.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserResponse, User>().IgnoreAllNonExisting().ReverseMap();
            CreateMap<UserResponse, UserRequest>().IgnoreAllNonExisting().ReverseMap();
        }
    }
}
