using DataAccess.Abstract;
using DataAccess.Concrete.EntityFrameworkCore.Contexts;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore.EfDals
{
    public class EfRoleDal : EfBaseDal<Role, Context>, IRoleDal
    {
    }
}
