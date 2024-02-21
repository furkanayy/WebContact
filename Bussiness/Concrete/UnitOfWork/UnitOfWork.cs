using Business.Abstract.Service;
using Business.Abstract.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private IUserService _userService;

        public UnitOfWork(DbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public IUserService UserManager { get { return _userService; } }
        //public int Complete()
        //{
        //    try
        //    {
        //        return _context.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}