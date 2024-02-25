using Business.Abstract.Service;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.Manager
{
    public class UserManager : IUserService
    {
        private IMapperService _mapperService;
        private IUserDal _userDal;
        public UserManager(IMapperService mapperService, IUserDal userDal)
        {
            _mapperService = mapperService;
            _userDal = userDal;
        }

        public UserDto Get(Expression<Func<User, bool>> filter)
        {
            var model = _userDal.Get(filter, include => include.Include(x => x.Role));
            return _mapperService.Map<User, UserDto>(model);
        }

        public ProfileDto GetProfile(Expression<Func<User, bool>> filter)
        {
            var model = _userDal.Get(filter);
            return _mapperService.Map<User, ProfileDto>(model);
        }

        public bool LoginControl(UserLoginDto userLoginDto)
        {
            var user = _userDal.Get(x => x.UserName == userLoginDto.UserName && x.Password == userLoginDto.Password && x.IsApproved);
            if (user != null)
                return true;
            return false;
        }

        public void AddUser(UserRegisterDto userRegisterDto)
        {
            var existingUser = _userDal.Get(x => x.UserName == userRegisterDto.UserName);
            if (existingUser != null)
            {
                throw new Exception("Bu kullanıcı adı zaten var. Lütfen başka bir kullanıcı adı seçin.");
            }

            // AutoMapper kullanarak User modeline dönüştür
            var newUser = _mapperService.Map<UserRegisterDto, User>(userRegisterDto);

            // IsApproved ve RoleId değerlerini ayarla
            newUser.IsApproved = false;
            newUser.RoleId = 3; // User rolü

            // Kullanıcıyı veritabanına ekle
            _userDal.Add(newUser);
        }

        public void UpdateUser(ProfileDto profileDto)
        {
            var existingUser = _userDal.Get(x => x.Id == profileDto.Id);
            var updatedUser = _mapperService.Map<ProfileDto, User>(profileDto, existingUser);
            _userDal.Update(updatedUser);
        }
    }
}
