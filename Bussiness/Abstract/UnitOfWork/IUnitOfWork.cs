using Business.Abstract.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.UnitOfWork
{
    public interface IUnitOfWork
    {
        //public int Complete();
        public IUserService UserManager { get; }
    }
}
