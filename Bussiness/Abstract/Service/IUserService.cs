using Entity.Concrete;
using Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.Service
{
    public interface IUserService
    {
        UserDto Get(Expression<Func<User, bool>> filter);
        bool LoginControl(UserLoginDto userLoginDto);
        void AddUser(UserRegisterDto userRegisterDto);
    }
}
