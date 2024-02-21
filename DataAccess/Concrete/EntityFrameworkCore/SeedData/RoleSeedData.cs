using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore.SeedData
{
        internal class RoleSeedData
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
               new Role { Id = 1, RoleName ="SuperAdmin" },
               new Role { Id = 2, RoleName = "Admin" },
               new Role { Id = 3, RoleName = "User" }
            );
        }
    }
}
