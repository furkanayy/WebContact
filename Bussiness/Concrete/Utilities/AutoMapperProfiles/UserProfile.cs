using AutoMapper;
using Entity.Concrete;
using Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.Utilities.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserLoginDto>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
        }
    }
}
